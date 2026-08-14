using DiscordLite.Application.Abstractions;

namespace DiscordLite.Infrastructure.Persistence;

public sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct) =>
        dbContext.SaveChangesAsync(ct);
}