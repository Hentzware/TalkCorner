using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkCorner.Domain.Entities;

namespace TalkCorner.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(p => p.Content, cb =>
        {
            cb
                .Property(c => c.Value)
                .HasColumnName("Content")
                .IsRequired();
        });

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Thread)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.ThreadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.ParentPost)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ParentPostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.Created)
            .IsRequired();
    }
}