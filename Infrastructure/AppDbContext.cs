using DiscordLite.Domain;
using Microsoft.EntityFrameworkCore;

namespace DiscordLite.Infrastructure;

public sealed class AppDbContext : DbContext
{   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
}