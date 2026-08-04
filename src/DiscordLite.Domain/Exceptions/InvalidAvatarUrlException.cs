namespace DiscordLite.Domain.Exceptions;

public sealed class InvalidAvatarUrlException(string avatarUrl) : DomainException($"Invalid avatar url: {avatarUrl}");