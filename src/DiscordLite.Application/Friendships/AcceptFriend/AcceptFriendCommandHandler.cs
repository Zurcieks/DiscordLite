using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using MediatR;
 

namespace DiscordLite.Application.Friendships.AcceptFriend
{
    public sealed class AcceptFriendCommandHandler(
        IFriendshipRepository friendshipRepository,
        ICurrentUser currentUser) : IRequestHandler<AcceptFriendCommand>
    {
        public async Task Handle(AcceptFriendCommand request, CancellationToken ct)
        {
            var userId = currentUser.UserId;
            
            var friendship = await friendshipRepository.GetByIdAsync(request.FriendshipId, ct);
            if(friendship is null)
                throw new NotFoundException("Friendship not found.");

            friendship.Accept(userId);

            await friendshipRepository.SaveChangesAsync(ct);
        }
    }
}
