using FluentValidation;

namespace DiscordLite.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }
}