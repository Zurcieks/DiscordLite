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

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasColumnName("id");

        builder.Property(user => user.Username)
            .HasColumnName("username")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(254)
            .IsRequired();

        builder.Property(user => user.AvatarUrl)
            .HasColumnName("avatar_url")
            .HasMaxLength(2_048);

        builder.Property(user => user.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(user => user.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasIndex(user => user.Username)
            .IsUnique();

        builder.HasIndex(user => user.Email)
            .IsUnique();
    }
}