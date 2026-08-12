namespace DiscordLite.Application.Exceptions;

public sealed  class NotFoundException(string code, string message) : 
    AppException(code, message);