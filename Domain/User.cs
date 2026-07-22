namespace DiscordLite.Domain;

public sealed class User
{
    
    public Guid Id { get; private set; }
    public string Username { get; private set; }  
    public string UsernameNormalized { get; private set; }
    public string? AvatarUrl { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    
    private User() { }

    public User(string username, string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username.Trim();
        UsernameNormalized = Normalize(Username);
        PasswordHash = passwordHash;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangeAvatar(string? avatarUrl)
    {
        AvatarUrl = avatarUrl;
    }

    
    public void ChangeUsername(string newUsername)
    {
        Username = newUsername.Trim();
        UsernameNormalized = Normalize(Username);
    }
    
    public static string Normalize(string value) => value.Trim().ToLowerInvariant();
    
}