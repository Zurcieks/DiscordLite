namespace DiscordLite.Application.Exceptions;

public sealed class ConflictException(string message) : AppException(message);
