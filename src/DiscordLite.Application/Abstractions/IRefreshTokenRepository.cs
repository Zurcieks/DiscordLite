using DiscordLite.Domain.Entities;

namespace DiscordLite.Application.Abstractions;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct);
    Task<int> RemoveExpiredAsync(CancellationToken ct);
}