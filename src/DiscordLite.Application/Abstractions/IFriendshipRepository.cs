using DiscordLite.Application.Friendships.GetAllFriends;
using DiscordLite.Application.Friendships.GetFriendsRequest;
using DiscordLite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Application.Abstractions
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Task<Friendship?> GetBetweenAsync(Guid userId1, Guid userId2, CancellationToken ct);
        Task<List<FriendRequestDto>> GetIncomingAndOutgoingRequests(Guid userId, CancellationToken ct);
        Task<Friendship?> GetByIdAsync(Guid friendshipId, CancellationToken ct);
        Task<List<FriendDto>> GetAllFriends(Guid userId, CancellationToken ct);
    }
}
