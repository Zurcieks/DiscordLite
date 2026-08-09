using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Application.Friendships.AddFriend;
using MediatR;
 

namespace DiscordLite.Application.Friendships.AcceptFriend
{
    public sealed class AcceptFriendCommandHandler(
        IFriendshipRepository friendshipRepository,
        ICurrentUser currentUser) : IRequestHandler<AcceptFriendCommand, string>
    {
        public async Task<string> Handle(AcceptFriendCommand request, CancellationToken ct)
        {
            var userId = currentUser.UserId;

            if (userId is null)
                throw new UnauthorizedAccessException();

            var friendship = await friendshipRepository.GetByIdAsync(request.FriendshipId, ct);
            if(friendship is null)
                throw new NotFoundException("Friendship not found.");

            friendship.Accept(userId.Value);

            await friendshipRepository.SaveChangesAsync(ct);
            return "Friendship accepted.";
        }
    }
}
