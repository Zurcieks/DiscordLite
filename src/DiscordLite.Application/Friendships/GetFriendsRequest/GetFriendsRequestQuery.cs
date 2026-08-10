using MediatR;

namespace DiscordLite.Application.Friendships.GetFriendsRequest
{
    public sealed record GetFriendsRequestQuery() : IRequest<GetFriendsRequestsResponse>;
    public sealed record FriendRequestDto(Guid FriendshipId, Guid UserId, string Username, string? AvatarUrl, DateTime createdAt, bool IsIncoming);
    public sealed record GetFriendsRequestsResponse(List<FriendRequestDto> Incoming, List<FriendRequestDto> Outgoing);
}
