namespace DiscordLite.Domain.Exceptions;

public sealed class InvalidUsernameException(string name) : DomainException($"Username '{name}' is invalid");