namespace DiscordLite.Domain.Exceptions;

public sealed class InvalidRefreshTokenException(string message) : DomainException(message);