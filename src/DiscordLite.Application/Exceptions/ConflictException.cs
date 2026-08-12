namespace DiscordLite.Application.Exceptions;

public sealed class ConflictException(string code, string message) : AppException(code, message);
