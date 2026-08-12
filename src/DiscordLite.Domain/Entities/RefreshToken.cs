using DiscordLite.Domain.Exceptions;

namespace DiscordLite.Domain.Entities;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    private RefreshToken() { }

    public static RefreshToken Create(
        Guid userId,
        string tokenHash,
        DateTime expiresAt)
    {
        if (userId == Guid.Empty)
            throw new DomainValidationException(
                "REFRESH_TOKEN_USER_ID_EMPTY",
                "UserId cannot be empty.");

        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new DomainValidationException(
                "REFRESH_TOKEN_HASH_EMPTY",
                "Token hash cannot be empty.");

        if (expiresAt <= DateTime.UtcNow)
            throw new DomainValidationException(
                "REFRESH_TOKEN_EXPIRATION_INVALID",
                "ExpiresAt must be in the future.");

        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow
        };
    }

    public bool IsActive =>
        RevokedAt is null && DateTime.UtcNow < ExpiresAt;

    public void Revoke()
    {
        if (RevokedAt is not null)
            return;

        RevokedAt = DateTime.UtcNow;
    }
}