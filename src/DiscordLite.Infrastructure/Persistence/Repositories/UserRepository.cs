using DiscordLite.Application.Abstractions;
using DiscordLite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordLite.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);
        return user;
    }

    public async Task<User?> GetByNormalizedUsernameAsync(string normalizedUsername, CancellationToken ct)
    {
        var user = await Context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.NormalizedUsername == normalizedUsername, ct);
        return user;
    }

    public async Task<bool> ExistsByNormalizedUsernameAsync(string normalizedUsername, CancellationToken ct)
    {
        var user = await Context.Users.AnyAsync(x => x.NormalizedUsername == normalizedUsername, ct);
        return user;
    }

   
}