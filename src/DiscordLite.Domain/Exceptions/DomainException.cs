namespace DiscordLite.Domain.Exceptions;

public abstract class DomainException(string code, string message) : Exception(message);
