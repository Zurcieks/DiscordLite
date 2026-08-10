using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;

namespace DiscordLite.Application.Friendships.CancelFriendRequest;

public sealed class CancelFriendRequestCommandHandler(
    IFriendshipRepository friendshipRepository,
    ICurrentUser currentUser) : IRequestHandler<CancelFriendRequestCommand>
{
    public async Task Handle(CancelFriendRequestCommand request, CancellationToken ct)
    {
        var userId = currentUser.UserId;

        var friendship = await friendshipRepository.GetByIdAsync(request.FriendshipId, ct);
        if (friendship is null)
            throw new NotFoundException("Friend request not found");

        if(friendship.Status != FriendshipStatus.Pending)
            throw new ConflictException("Only pending friend requests can be canceled.");
        
        if(userId != friendship.SenderId)
            throw new ForbiddenException("Only the sender can cancel this friend request.");
        
        friendshipRepository.Delete(friendship);
        
        await friendshipRepository.SaveChangesAsync(ct);
    }
}