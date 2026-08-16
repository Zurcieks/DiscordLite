using DiscordLite.Application.Abstractions;

namespace DiscordLite.Api.Security;

public class RefreshTokenCookieWriter(IHttpContextAccessor contextAccessor) : IRefreshTokenCookieWriter
{

    private const string CookieName = "refreshToken";

    public void Write(string refreshToken)
    {
        var context = contextAccessor.HttpContext
                      ?? throw new InvalidOperationException("HttpContext is required.");

        context.Response.Cookies.Append(CookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            Path = "/api/auth"
        });
    }

    public void Remove()
    {
        var context = contextAccessor.HttpContext
                      ?? throw new InvalidOperationException("HttpContext is required.");

        context.Response.Cookies.Delete(
            CookieName,
            new CookieOptions
            {
                Path = "/api/auth"
            });
    }
}