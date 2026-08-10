using DiscordLite.Application.Abstractions;
using MediatR;
using DiscordLite.Application.Exceptions;

namespace DiscordLite.Application.Friendships.GetAllFriends
{
    public sealed class GetAllFriendsQueryHandler(
        IFriendshipRepository friendshipRepository,
        ICurrentUser currentUser) : IRequestHandler<GetAllFriendsQuery, GetAllFriendResponse>
    {
        public async Task<GetAllFriendResponse> Handle(GetAllFriendsQuery request, CancellationToken ct)
        {
            var userId = currentUser.UserId;
            var allFriends = await friendshipRepository.GetAllFriends(userId, ct);

            return new GetAllFriendResponse(allFriends);
        }
    }
}
