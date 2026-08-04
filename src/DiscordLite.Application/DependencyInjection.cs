using System.Reflection;
using DiscordLite.Application.Abstractions;
using DiscordLite.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordLite.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly =
            Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(applicationAssembly);
            
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            
        });
        
        services.AddValidatorsFromAssembly(applicationAssembly);
        return services;
    }
}