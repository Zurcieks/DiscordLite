namespace DiscordLite.Application.Abstractions;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct);
    void Remove(T entity);
    Task SaveChangesAsync(CancellationToken ct);
}