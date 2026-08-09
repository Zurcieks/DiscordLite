using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;

namespace DiscordLite.Application.Users.GetMyProfile;

public sealed class GetMyProfileQueryHandler(
    ICurrentUser currentUser,
    IUserRepository userRepository) : IRequestHandler<GetMyProfileQuery, UserProfileResponse>
{
    public async Task<UserProfileResponse> Handle(GetMyProfileQuery request, CancellationToken ct)
    {

        var userId = currentUser.UserId;
        if (userId is null)
            throw new UnauthorizedException("User not found");

        var user = await userRepository.GetByIdAsync(userId.Value, ct)
                   ?? throw new NotFoundException("User not found.");

        return new UserProfileResponse(user.Id, user.Username, user.AvatarUrl);
    }
}