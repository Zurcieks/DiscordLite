namespace DiscordLite.Domain.Exceptions;

public sealed class DomainConflictException(string code, string message) : DomainException(code, message);
