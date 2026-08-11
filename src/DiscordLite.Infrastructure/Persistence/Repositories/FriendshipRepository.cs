using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Friendships.GetAllFriends;
using DiscordLite.Application.Friendships.GetFriendsRequest;
using DiscordLite.Domain.Entities;
using DiscordLite.Infrastructure.Persistence;
using DiscordLite.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Infrastructure.Persistence.Repositories
{
    public sealed class FriendshipRepository(AppDbContext context) : RepositoryBase<Friendship>(context), IFriendshipRepository
    {
        public async Task<List<FriendDto>> GetAllFriends(Guid userId, CancellationToken ct)
        {
            return await Context.Friendships
                .Where(x => x.Status == FriendshipStatus.Accepted && 
                (x.SenderId == userId || x.ReceiverId == userId)) // jedna z stron relacji
                .Join(
                Context.Users,
                friends => friends.SenderId == userId ? friends.ReceiverId : friends.SenderId, 
                user => user.Id,
                (friends, user) => new FriendDto
                (
                    user.Id,
                    user.Username,
                    user.AvatarUrl
                )).ToListAsync(ct);
                   
        }


        public async Task<Friendship?> GetBetweenAsync(Guid userId1, Guid userId2, CancellationToken ct)
        {
            return await Context.Friendships
                .FirstOrDefaultAsync(x => (x.SenderId == userId1 && x.ReceiverId == userId2) || (x.SenderId == userId2 && x.ReceiverId == userId1), ct);
        }

        public async Task<Friendship?> GetByIdAsync(Guid friendshipId, CancellationToken ct)
        {
            return await Context.Friendships
                .FirstOrDefaultAsync(x => x.Id == friendshipId, ct);
        }

        public async Task<List<FriendRequestDto>> GetIncomingAndOutgoingRequests(Guid userId, CancellationToken ct)
        {
            return await Context.Friendships
                .Where(x => x.Status == FriendshipStatus.Pending && (x.SenderId == userId || x.ReceiverId == userId))
                .Join(
                Context.Users,
                friends => friends.SenderId == userId ? friends.ReceiverId : friends.SenderId,
                user => user.Id,
                (friends, user) => new FriendRequestDto
                (
                    friends.Id,
                    user.Id,
                    user.Username,
                    user.AvatarUrl,
                    friends.CreatedAt,
                    friends.ReceiverId == userId
                )).ToListAsync(ct);
                   
        }
    }
}
