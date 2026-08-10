using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Application.Friendships.GetAllFriends
{
    public sealed record GetAllFriendsQuery() : IRequest<GetAllFriendResponse>;
    public sealed record GetAllFriendResponse(List<FriendDto> Friends);
    public sealed record FriendDto(Guid UserId, string Username, string? AvatarUrl);

}
