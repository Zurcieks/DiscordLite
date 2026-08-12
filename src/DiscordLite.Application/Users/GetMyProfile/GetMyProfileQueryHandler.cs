using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using MediatR;

namespace DiscordLite.Application.Users.GetMyProfile;

public sealed class GetMyProfileQueryHandler(
    ICurrentUser currentUser,
    IUserRepository userRepository)
    : IRequestHandler<GetMyProfileQuery, UserProfileResponse>
{
    public async Task<UserProfileResponse> Handle(
        GetMyProfileQuery request,
        CancellationToken ct)
    {
        var userId = currentUser.UserId;

        var user = await userRepository.GetByIdAsync(userId, ct)
                   ?? throw new NotFoundException(
                       "USER_NOT_FOUND",
                       "User not found.");

        return new UserProfileResponse(
            user.Id,
            user.Username,
            user.AvatarUrl);
    }
}