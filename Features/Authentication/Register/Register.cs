using DiscordLite.Common;
using DiscordLite.Common.Interfaces;
using DiscordLite.Domain;
using DiscordLite.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DiscordLite.Features.Authentication.Register;

public record RegisterRequest(string Username, string Password);


public static class Register
{

    public static IEndpointRouteBuilder RegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/register", Handle)
            .WithTags("Authentication")
            .WithName("Register")
            .WithValidation<RegisterRequest>();
        return app;
    }
    
    private static async Task<IResult> Handle(RegisterRequest request, IPasswordHasher hasher ,AppDbContext context, CancellationToken ct)
    {
        var usernameNormalized = User.Normalize(request.Username);

        var exists = await context.Users
            .AnyAsync(u => u.UsernameNormalized == usernameNormalized, ct);

        if (exists)
        {
            return Results.Conflict(new
            {
                message = "Username already in use",
                field = "Username"
            });
        }
        
        var passwordHash = hasher.HashPassword(request.Password);
        var user = new User(request.Username, passwordHash);

        context.Users.Add(user);

        try
        {
            await context.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException {SqlState: PostgresErrorCodes.UniqueViolation})
        {
            return Results.Conflict(new
            {
                message = "Username already in use",
                field = "Username"
            });
        }
        return Results.Created($"/api/users/{user.Id}", new
        {
            user.Id,
            user.Username
        });
    }
}

