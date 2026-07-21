using DiscordLite.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
 

namespace  DiscordLite.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPasswordHasher, IPasswordHasher>();

        return services;
    }
}