namespace DiscordLite.Application.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string username);
    string GenerateRefreshToken();
    string HashRefreshToken(string refreshToken);
    DateTime GetRefreshTokenExpiry(); 
}