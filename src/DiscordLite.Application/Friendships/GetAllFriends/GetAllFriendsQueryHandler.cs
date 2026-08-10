using DiscordLite.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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
            if (userId is null)
                throw new UnauthorizedException("User is not authenticated.");

            var allFriends = await friendshipRepository.GetAllFriends(userId.Value, ct);

            return new GetAllFriendResponse(allFriends);
        }
    }
}
