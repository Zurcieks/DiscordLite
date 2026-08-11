using MediatR;

namespace DiscordLite.Application.Friendships.RejectFriendRequest;

public sealed record RejectFriendRequestCommand(Guid FriendshipId) : IRequest;