using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkCorner.Domain.Entities;

namespace TalkCorner.Persistence.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.ToTable("Boards");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(b => b.Title, tb =>
        {
            tb
                .Property(t => t.Value)
                .HasColumnName("Title")
                .IsRequired();
        });

        builder.OwnsOne(b => b.Description, db =>
        {
            db
                .Property(d => d.Value)
                .HasColumnName("Description")
                .IsRequired();
        });

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany(x => x.CreatedBoards)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Moderators)
            .WithMany(x => x.ModeratedBoards)
            .UsingEntity(x => x.ToTable("BoardModerators"));

        builder
            .HasOne(x => x.ParentBoard)
            .WithMany(x => x.SubBoards)
            .HasForeignKey(x => x.ParentBoardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.Created)
            .IsRequired();
    }
}