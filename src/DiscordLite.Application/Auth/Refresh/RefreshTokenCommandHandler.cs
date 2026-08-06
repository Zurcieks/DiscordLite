using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Entities;
using MediatR;

namespace DiscordLite.Application.Auth.Refresh;

public sealed class RefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    ITokenService tokenService) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var tokenHash = tokenService.HashRefreshToken(request.RefreshToken);

        var existingToken = await refreshTokenRepository.GetByTokenHashAsync(tokenHash, ct);

        if (existingToken is null || !existingToken.IsActive)
            throw new UnauthorizedException("Invalid or expired refresh token.");

        var user = await userRepository.GetByIdAsync(existingToken.UserId, ct)
                   ?? throw new UnauthorizedException("Invalid or expired refresh token.");

        existingToken.Revoke();

        var newRefreshTokenPlain = tokenService.GenerateRefreshToken();
        var newRefreshTokenHash = tokenService.HashRefreshToken(newRefreshTokenPlain);

        var newRefreshToken = RefreshToken.Create(
            user.Id,
            newRefreshTokenHash,
            tokenService.GetRefreshTokenExpiry());

        await refreshTokenRepository.AddAsync(newRefreshToken, ct);
        await refreshTokenRepository.SaveChangesAsync(ct);

        var newAccessToken = tokenService.GenerateAccessToken(user.Id, user.Username);

        return new RefreshTokenResponse(newAccessToken, newRefreshTokenPlain);
    }
}