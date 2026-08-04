namespace DiscordLite.Application.Abstractions;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}