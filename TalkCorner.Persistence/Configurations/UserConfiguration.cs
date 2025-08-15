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

        builder
            .Property(x => x.Updated)
            .IsRequired();

        var adminId = Guid.Parse("2FC3BA78-D903-4BFF-A579-276F6269167E");
        var adminAppUserId = Guid.Parse("C99AC715-51B2-400E-9625-290D19418B70");
        var userId = Guid.Parse("64BE4B49-60BD-4779-984A-2C5B40DECE32");
        var userAppUserId = Guid.Parse("4895AD73-D889-49DE-9418-E8B163BEACB5");
        var createdAt = new DateTime(2025, 08, 01, 0, 0, 0, DateTimeKind.Utc);

        builder.OwnsOne(u => u.DisplayName, db =>
        {
            db
                .Property(d => d.Value)
                .HasColumnName("DisplayName")
                .IsRequired();

            db.HasData(
                new { UserId = adminId, Value = "Administrator" },
                new { UserId = userId, Value = "User" }
            );
        });

        builder.HasData(
            new { Id = adminId, ApplicationUserId = adminAppUserId, Created = createdAt, Updated = createdAt },
            new { Id = userId, ApplicationUserId = userAppUserId, Created = createdAt, Updated = createdAt }
            );
    }
}