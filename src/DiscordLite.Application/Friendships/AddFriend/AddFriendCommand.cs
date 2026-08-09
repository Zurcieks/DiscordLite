using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DiscordLite.Application.Friendships.AddFriend
{
    public sealed record AddFriendCommand(string Username) : IRequest<string>;
}
