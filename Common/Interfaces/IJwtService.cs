namespace DiscordLite.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(Guid userId, string username);
}