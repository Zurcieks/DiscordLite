namespace DiscordLite.Common;

public static class ValidationExtensions
{
    public static RouteHandlerBuilder WithValidation<TRequest>(
        this RouteHandlerBuilder builder)
        where TRequest : class
    {
        return builder.AddEndpointFilter<ValidationFilter<TRequest>>();
    }
}