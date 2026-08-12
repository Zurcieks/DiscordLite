namespace DiscordLite.Application.Abstractions;

public interface IRefreshTokenCookieWriter
{
    void Write(string refreshToken);
}