using System.Security.Claims;
using DiscordLite.Application.Abstractions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DiscordLite.Api.Security;

public class CurrentUser(IHttpContextAccessor accessor): ICurrentUser
{
    public Guid? UserId
    {
        get
        {
            var userIdClaims = accessor.HttpContext?
                .User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            
            return Guid.TryParse(userIdClaims, out var userId) ? userId : null;
        }
    }
}