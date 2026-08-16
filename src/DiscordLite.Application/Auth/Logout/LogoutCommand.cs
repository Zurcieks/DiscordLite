using MediatR;

namespace DiscordLite.Application.Auth.Logout;

public sealed record LogoutCommand(string RefreshToken) : IRequest;