using MediatR;

namespace DiscordLite.Application.Auth.Login;

public sealed record LoginUserCommand(string Username, string Password) : IRequest<LoginUserResponse>;

public sealed record LoginUserResponse(Guid UserId, string Username, string AccessToken);