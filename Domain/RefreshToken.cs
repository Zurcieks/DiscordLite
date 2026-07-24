using System.Security.Cryptography;

namespace DiscordLite.Domain;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? RevokedAt  { get; private set; }
    
    public bool IsActive => RevokedAt is null && DateTimeOffset.UtcNow < ExpiresAt;
    
    private RefreshToken() {}

    public RefreshToken(Guid userId, TimeSpan lifetime)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = GenerateToken();
        ExpiresAt = DateTimeOffset.UtcNow.Add(lifetime);
    }
    
    public void Revoke() => RevokedAt = DateTimeOffset.UtcNow;

    private static string GenerateToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    
}