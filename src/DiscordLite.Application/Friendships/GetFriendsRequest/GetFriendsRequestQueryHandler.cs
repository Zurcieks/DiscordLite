using DiscordLite.Application.Abstractions;
using MediatR;
 

namespace DiscordLite.Application.Friendships.GetFriendsRequest
{
    public class GetFriendsRequestQueryHandler(
        IFriendshipRepository friendshipRepository,
        ICurrentUser currentUser) : IRequestHandler<GetFriendsRequestQuery, GetFriendsRequestsResponse>
    {
        public async Task<GetFriendsRequestsResponse> Handle(GetFriendsRequestQuery request, CancellationToken ct)
        {
            var userId = currentUser.UserId;
            
            var requests = await friendshipRepository.GetIncomingAndOutgoingRequests(userId, ct);

            var incoming = requests.Where(x => x.IsIncoming).ToList();
            var outgoing = requests.Where(x => !x.IsIncoming).ToList();

            return new GetFriendsRequestsResponse(incoming, outgoing);
        }


    }
}
