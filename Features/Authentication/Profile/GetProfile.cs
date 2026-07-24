using System.Security.Claims;
using DiscordLite.Common;
using DiscordLite.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DiscordLite.Features.Authentication.Profile;

public static class GetProfile
{
    public static IEndpointRouteBuilder MapGetProfileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/me", Handle)
            .WithTags("Authentication")
            .WithName("GetProfile")
            .RequireAuthorization();

        return app;
    }
    
    private static async Task<IResult> Handle(
        ClaimsPrincipal claims,
        AppDbContext context,
        CancellationToken ct)
    {
        var userId = claims.GetUserId();

        var user = await context.Users
            .Where(u => u.Id == userId)
            .Select(u => new { u.Id, u.Username, u.AvatarUrl, u.CreatedAt })
            .FirstOrDefaultAsync(ct);

        if (user is null)   
            return Results.NotFound();

        return Results.Ok(user);
    }
}