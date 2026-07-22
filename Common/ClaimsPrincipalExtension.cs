using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiscordLite.Common;

public static class ClaimsPrincipalExtension
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var value = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (value is null || !Guid.TryParse(value, out var userId))
            throw new UnauthorizedAccessException("User id claim is missing or invalid");
        
        return userId;
    }
}