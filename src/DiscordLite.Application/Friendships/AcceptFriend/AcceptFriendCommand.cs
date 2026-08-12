using MediatR;

namespace DiscordLite.Application.Friendships.AcceptFriend
{
    public sealed record AcceptFriendCommand(Guid FriendshipId) : IRequest;

}
