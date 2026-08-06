using MediatR;

namespace DiscordLite.Application.Users.GetMyProfile;

public sealed record GetMyProfileQuery : IRequest<UserProfileResponse>;

public sealed record UserProfileResponse(Guid Id, string Username, string? AvatarUrl);