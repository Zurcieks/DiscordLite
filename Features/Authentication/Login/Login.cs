using DiscordLite.Common;
using DiscordLite.Common.Interfaces;
using DiscordLite.Domain;
using DiscordLite.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace DiscordLite.Features.Authentication.Login;

public record LoginRequest(string Username, string Password);

public static class Login
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", Handle)
            .WithTags("Authentication")
            .WithName("Login")
            .WithValidation<LoginRequest>();

        return app;
    }

    private static async Task<IResult> Handle(
        LoginRequest request,
        IPasswordHasher hasher,
        IJwtService jwtService,
        AppDbContext context,
        CancellationToken ct)
    {
        var usernameNormalized = User.Normalize(request.Username);

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.UsernameNormalized == usernameNormalized, ct);

        if (user is null)
            return Results.Unauthorized();

        var isPasswordValid = hasher.VerifyPassword(user.PasswordHash, request.Password);

        if (!isPasswordValid)
            return Results.Unauthorized();

        var accessToken = jwtService.GenerateAccessToken(user.Id, user.Username);

        return Results.Ok(new
        {
            accessToken,
            user.Id,
            user.Username,
        });
    }
}