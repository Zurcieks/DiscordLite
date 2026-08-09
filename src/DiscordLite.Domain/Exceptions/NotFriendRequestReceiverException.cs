using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Domain.Exceptions
{
    public sealed class NotFriendRequestReceiverException(string message) : DomainException(message);

}
