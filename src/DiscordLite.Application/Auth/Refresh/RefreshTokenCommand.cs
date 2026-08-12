using MediatR;

namespace DiscordLite.Application.Auth.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResponse>;

public sealed record RefreshTokenResponse(string AccessToken);