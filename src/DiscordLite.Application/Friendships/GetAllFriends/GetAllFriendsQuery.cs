using MediatR;


namespace DiscordLite.Application.Friendships.GetAllFriends
{
    public sealed record GetAllFriendsQuery() : IRequest<GetAllFriendResponse>;
    public sealed record GetAllFriendResponse(List<FriendshipDto> Friends);
    public sealed record FriendshipDto(Guid FriendshipId, Guid UserId, string Username, string? AvatarUrl);

}
