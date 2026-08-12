namespace DiscordLite.Application.Exceptions;

public abstract class AppException(string code, string message) : Exception(message);
