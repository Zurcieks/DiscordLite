using MediatR;

namespace DiscordLite.Application.Friendships.DeleteFriend;

public sealed record DeleteFriendCommand(Guid FriendshipId) : IRequest;
