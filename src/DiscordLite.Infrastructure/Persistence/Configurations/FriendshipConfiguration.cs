using DiscordLite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordLite.Infrastructure.Persistence.Configurations
{
    public sealed class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.ReceiverId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SenderId);
            builder.HasIndex(x => x.ReceiverId);
            builder.HasIndex(x => new { x.SenderId, x.ReceiverId }).IsUnique();



        }
    }
}
