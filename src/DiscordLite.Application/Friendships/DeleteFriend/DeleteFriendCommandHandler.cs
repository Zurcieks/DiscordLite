using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;

namespace DiscordLite.Application.Friendships.DeleteFriend;

public class DeleteFriendCommandHandler(
    IFriendshipRepository friendshipRepository,
    ICurrentUser currentUser): IRequestHandler<DeleteFriendCommand>
{
    public async Task Handle(DeleteFriendCommand request, CancellationToken ct)
    {
        var userId =  currentUser.UserId;
        
        var friendship = await friendshipRepository.GetByIdAsync(request.FriendshipId, ct);
        
        if (friendship is null)
            throw new NotFoundException("Friendship not found.");
        
        if (friendship.SenderId != userId && friendship.ReceiverId != userId)
            throw new ForbiddenException("You are not part of this friendship.");
        
        if (friendship.Status != FriendshipStatus.Accepted)
            throw new ConflictException("Only accepted friendships can be deleted.");
        
        friendshipRepository.Remove(friendship);
        
        await friendshipRepository.SaveChangesAsync(ct);



    }
}