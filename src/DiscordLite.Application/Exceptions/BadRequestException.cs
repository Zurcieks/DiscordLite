namespace DiscordLite.Application.Exceptions;

public sealed class BadRequestException(string message) : AppException(message);
