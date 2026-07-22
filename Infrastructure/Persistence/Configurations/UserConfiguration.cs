using DiscordLite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscordLite.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.UsernameNormalized).IsRequired().HasMaxLength(50);
        
        
        builder.Property(x => x.AvatarUrl)
            .HasMaxLength(2_048);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.UsernameNormalized)
            .IsUnique();
        
    }
}