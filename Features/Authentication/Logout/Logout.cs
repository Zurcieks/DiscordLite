using DiscordLite.Common;
using DiscordLite.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DiscordLite.Features.Authentication.Logout;

public record LogoutRequest(string RefreshToken);

public static class Logout
{
    public static IEndpointRouteBuilder MapLogoutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/logout", Handle)
            .WithTags("Authentication")
            .WithName("Logout")
            .WithValidation<LogoutRequest>();
        return app;
    }

    private static async Task<IResult> Handle(
        LogoutRequest request,
        AppDbContext context,
        CancellationToken ct)
    {
        var storedToken = await context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken, ct);

        if (storedToken is not null)
        {
            storedToken.Revoke();
            await context.SaveChangesAsync(ct);
        }

        return Results.NoContent(); 
    }
}