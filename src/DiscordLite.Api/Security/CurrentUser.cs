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
                .User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            
            return Guid.TryParse(userIdClaims, out var userId) ? userId : null;
        }
    }
}