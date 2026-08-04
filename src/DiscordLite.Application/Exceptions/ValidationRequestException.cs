namespace DiscordLite.Application.Exceptions;

public sealed class ValidationRequestException(IDictionary<string, string[]> errors)
    : AppException("One or more validation errors occurred.")
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}