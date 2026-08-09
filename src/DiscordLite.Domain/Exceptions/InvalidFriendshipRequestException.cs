using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Domain.Exceptions
{
   public sealed class InvalidFriendshipRequestException(string message) : DomainException(message);
     
}
