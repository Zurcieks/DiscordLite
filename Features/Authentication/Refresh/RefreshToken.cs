// Features/Authentication/RefreshToken/RefreshToken.cs
using DiscordLite.Common;
using DiscordLite.Common.Interfaces;
using DiscordLite.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DiscordLite.Features.Authentication.Refresh;

public record RefreshTokenRequest(string RefreshToken);

public static class RefreshToken
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/refresh", Handle)
            .WithTags("Authentication")
            .WithName("RefreshToken")
            .WithValidation<RefreshTokenRequest>();
        return app;
    }

    private static async Task<IResult> Handle(
        RefreshTokenRequest request,
        AppDbContext context,
        IJwtService jwtService,
        CancellationToken ct)
    {
        var storedToken = await context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken, ct);

        if (storedToken is null || !storedToken.IsActive)
            return Results.Unauthorized();

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == storedToken.UserId, ct);

        if (user is null)
            return Results.Unauthorized();

        var newAccessToken = jwtService.GenerateAccessToken(user.Id, user.Username);

        return Results.Ok(new
        {
            accessToken = newAccessToken,
            refreshToken = storedToken.Token 
        });
    }
}