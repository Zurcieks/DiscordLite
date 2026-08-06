using DiscordLite.Domain.Entities;

namespace DiscordLite.Application.Abstractions;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task<User?> GetByNormalizedUsernameAsync(string normalizedUsername, CancellationToken ct);
    Task<bool> ExistsByNormalizedUsernameAsync(string normalizedUsername, CancellationToken ct);
}