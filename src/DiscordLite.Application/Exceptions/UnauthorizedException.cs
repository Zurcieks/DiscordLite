namespace DiscordLite.Application.Exceptions;

public sealed class UnauthorizedException(string code, string message) : AppException(code, message);
