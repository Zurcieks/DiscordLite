namespace DiscordLite.Domain.Exceptions;

public sealed class DomainForbiddenException(string code, string message) : DomainException(code, message);
