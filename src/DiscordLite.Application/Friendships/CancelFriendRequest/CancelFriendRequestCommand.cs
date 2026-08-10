using MediatR;

namespace DiscordLite.Application.Friendships.CancelFriendRequest;

public sealed record CancelFriendRequestCommand(Guid FriendshipId) : IRequest;