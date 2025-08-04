using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Persistence.Configurations;

public class ThreadConfiguration : IEntityTypeConfiguration<Thread>
{
    public void Configure(EntityTypeBuilder<Thread> builder)
    {
        builder.ToTable("Threads");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(t => t.Title, tb =>
        {
            tb
                .Property(ti => ti.Value)
                .HasColumnName("Title")
                .IsRequired();
        });

        builder
            .HasOne(x => x.Board)
            .WithMany(x => x.Threads)
            .HasForeignKey(x => x.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany(x => x.Threads)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.Thread)
            .HasForeignKey(x => x.ThreadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(x => x.Created)
            .IsRequired();
    }
}