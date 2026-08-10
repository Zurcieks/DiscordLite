using System.Security.Claims;
using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Exceptions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DiscordLite.Api.Security;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public Guid UserId
    {
        get
        {
            var userIdClaim = accessor.HttpContext?
                .User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return !Guid.TryParse(userIdClaim, out var userId) ? throw new UnauthorizedException("Invalid user id claim.") : userId;
        }
    }
}