using DiscordLite.Application.Abstractions;

namespace DiscordLite.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<T>(AppDbContext context): IRepository<T> where T : class
{
    protected readonly AppDbContext Context = context;

    public async Task AddAsync(T entity, CancellationToken ct)
    {
        await Context.Set<T>().AddAsync(entity, ct);
    }
    
    public Task SaveChangesAsync(CancellationToken ct) => Context.SaveChangesAsync(ct);
}