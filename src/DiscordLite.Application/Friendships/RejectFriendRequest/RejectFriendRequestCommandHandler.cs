using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;

namespace DiscordLite.Application.Friendships.RejectFriendRequest;

public class RejectFriendRequestCommandHandler(
    IFriendshipRepository friendshipRepository,
    ICurrentUser currentUser)
    : IRequestHandler<RejectFriendRequestCommand>
{
    public async Task Handle(
        RejectFriendRequestCommand request,
        CancellationToken ct)
    {
        var userId = currentUser.UserId;

        var friendship = await friendshipRepository
            .GetByIdAsync(request.FriendshipId, ct);

        if (friendship is null)
        {
            throw new NotFoundException(
                "FRIENDSHIP_REQUEST_NOT_FOUND",
                "Friend request not found.");
        }

        if (userId != friendship.ReceiverId)
        {
            throw new ForbiddenException(
                "FRIENDSHIP_NOT_REQUEST_RECEIVER",
                "Only the receiver can reject this friend request.");
        }

        if (friendship.Status != FriendshipStatus.Pending)
        {
            throw new ConflictException(
                "FRIENDSHIP_REQUEST_NOT_PENDING",
                "Only pending friend requests can be rejected.");
        }

        friendshipRepository.Remove(friendship);

        await friendshipRepository.SaveChangesAsync(ct);
    }
}