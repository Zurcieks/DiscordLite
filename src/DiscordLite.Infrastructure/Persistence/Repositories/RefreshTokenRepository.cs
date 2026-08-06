using DiscordLite.Application.Abstractions;
using DiscordLite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordLite.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository(AppDbContext context)
    :RepositoryBase<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct) =>
        await Context.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash, ct);

    public async Task<int> RemoveExpiredAsync(CancellationToken ct) =>
        await Context.RefreshTokens
            .Where(x => x.ExpiresAt < DateTime.UtcNow)
            .ExecuteDeleteAsync(ct);
}