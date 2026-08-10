namespace DiscordLite.Application.Abstractions;

public interface ICurrentUser
{
    Guid UserId{ get; }
}