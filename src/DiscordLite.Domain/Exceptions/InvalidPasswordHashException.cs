namespace DiscordLite.Domain.Exceptions;

public sealed class InvalidPasswordHashException(string message) : DomainException(message);