namespace DiscordLite.Application.Exceptions;

public sealed  class NotFoundException(string entity, object key) : 
    AppException($"{entity} with id '{key}' was not found.");