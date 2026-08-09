using DiscordLite.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Domain.Entities
{
    public sealed class Friendship
    {
        public Guid Id { get; private set; }
        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public FriendshipStatus Status { get; private set; }
        public DateTime? RespondedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Friendship() { }

        public static Friendship Create(Guid senderId, Guid receiverId)
        {
            if(Guid.Empty == senderId)
                throw new InvalidUserIdException("SenderId cannot be empty.");

            if (Guid.Empty == receiverId)
                throw new InvalidUserIdException("ReceiverId cannot be empty.");
            if(senderId == receiverId)
                throw new SelfFriendRequestException("SenderId and ReceiverId cannot be the same.");

            return new Friendship
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendshipStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Accept(Guid receiverId)
        {
            if (Guid.Empty == receiverId)
                throw new InvalidUserIdException("ReceiverId cannot be empty.");

            if (receiverId != ReceiverId)
                throw new NotFriendRequestReceiverException("Only the receiver can accept the friend request.");

            if(Status != FriendshipStatus.Pending)
                throw new InvalidFriendshipRequestException("Only pending friend requests can be accepted.");

            Status = FriendshipStatus.Accepted;
            RespondedAt = DateTime.UtcNow;

        }
        
    }

    public enum FriendshipStatus
    {
        Pending,
        Accepted,
    }
}
