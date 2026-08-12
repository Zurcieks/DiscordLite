using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;

namespace DiscordLite.Application.Friendships.CancelFriendRequest;

public sealed class CancelFriendRequestCommandHandler(
    IFriendshipRepository friendshipRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CancelFriendRequestCommand>
{
    public async Task Handle(
        CancelFriendRequestCommand request,
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

        if (userId != friendship.SenderId)
        {
            throw new ForbiddenException(
                "FRIENDSHIP_NOT_REQUEST_SENDER",
                "Only the sender can cancel this friend request.");
        }

        if (friendship.Status != FriendshipStatus.Pending)
        {
            throw new ConflictException(
                "FRIENDSHIP_REQUEST_NOT_PENDING",
                "Only pending friend requests can be canceled.");
        }

        friendshipRepository.Remove(friendship);

        await friendshipRepository.SaveChangesAsync(ct);
    }
}