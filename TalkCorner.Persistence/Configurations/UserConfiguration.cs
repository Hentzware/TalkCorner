using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkCorner.Domain.Entities;

namespace TalkCorner.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.ApplicationUserId)
            .IsRequired();

        builder.OwnsOne(u => u.DisplayName, db =>
        {
            db
                .Property(d => d.Value)
                .HasColumnName("DisplayName")
                .IsRequired();
        });

        builder
            .HasMany(x => x.CreatedBoards)
            .WithOne(x => x.CreatedByUser)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.ModeratedBoards)
            .WithMany(x => x.Moderators)
            .UsingEntity(x => x.ToTable("BoardModerators"));

        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.CreatedByUser)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Threads)
            .WithOne(x => x.CreatedByUser)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.Created)
            .IsRequired();
    }
}