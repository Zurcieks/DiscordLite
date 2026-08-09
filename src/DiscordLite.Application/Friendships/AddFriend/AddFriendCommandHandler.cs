using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Application.Friendships.AddFriend
{
    public sealed class AddFriendCommandHandler(
        IFriendshipRepository friendshipRepository,
        IUserRepository userRepository,
        ICurrentUser currentUser) : IRequestHandler<AddFriendCommand, string>
    {
        public async Task<string> Handle(AddFriendCommand request, CancellationToken ct)
        {
            var sender = currentUser.UserId;
            if (sender == null)
                throw new UnauthorizedException("User is not authenticated.");

            var normalized = User.NormalizeUsername(request.Username);
            var receiver = await userRepository.GetByNormalizedUsernameAsync(normalized, ct);

            if (receiver == null)
                throw new NotFoundException("User not found.");

            var exists = await friendshipRepository.GetBetweenAsync(sender.Value, receiver.Id, ct);

            if (exists != null && exists.Status == FriendshipStatus.Pending && sender.Value == exists.SenderId)
                throw new ConflictException("Friend request already sent.");

            if (exists != null && exists.Status == FriendshipStatus.Pending && sender.Value == exists.ReceiverId)
                throw new ConflictException("You have a pending friend request from this user.");
            if (exists != null && exists.Status == FriendshipStatus.Accepted)
                throw new ConflictException("You are already friends with this user.");


            var friendship = Friendship.Create(sender.Value, receiver.Id);

            await friendshipRepository.AddAsync(friendship, ct);
            await friendshipRepository.SaveChangesAsync(ct);

            return "Friend request sent successfully.";

        }
    }
}
