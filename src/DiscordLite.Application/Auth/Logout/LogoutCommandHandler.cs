using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using MediatR;

namespace DiscordLite.Application.Auth.Logout;

public class LogoutCommandHandler(  
    IRefreshTokenRepository refreshTokenRepository,
    ITokenService tokenService,
    IRefreshTokenCookieWriter cookieWriter,
    IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken ct)
    {
        var tokenHash = tokenService.HashRefreshToken(request.RefreshToken);

        var existingToken = await refreshTokenRepository
            .GetByTokenHashAsync(tokenHash, ct);
        
        if (existingToken is not null && existingToken.IsActive)
        {
            existingToken.Revoke();
            await unitOfWork.SaveChangesAsync(ct);
        }

        cookieWriter.Remove();
    }
}