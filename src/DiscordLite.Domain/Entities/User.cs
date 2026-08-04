using DiscordLite.Domain.Exceptions;

namespace DiscordLite.Domain.Entities;

public sealed class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = null!;
    public string NormalizedUsername { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string? AvatarUrl { get; private set; }
    
    private User() {}
 
    public static User Create(string username, string passwordHash)
    {
        if(string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 30)
            throw new InvalidUsernameException(username);
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidPasswordHashException("PasswordHash cannot be null or empty");
        
        return new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            NormalizedUsername = NormalizeUsername(username),
            PasswordHash = passwordHash,
        };
    }

    public void ChangeAvatar(string? avatarUrl)
    {
        if(avatarUrl is not null && string.IsNullOrWhiteSpace(avatarUrl))
            throw new InvalidAvatarUrlException(avatarUrl);
        
        AvatarUrl = avatarUrl;
    }
    
    public static string NormalizeUsername(string username) => username.Trim().ToLowerInvariant();
     

}