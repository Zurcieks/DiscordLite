namespace DiscordLite.Domain;

public sealed class User
{
    
    private User() { }

    public User(string username, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public Guid Id { get; set; }
    public string Username { get; set; }  
    public string Email { get; set; } 
    public string? AvatarUrl { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public void ChangeAvatar(string? avatarUrl)
    {
        AvatarUrl = avatarUrl;
    }
}