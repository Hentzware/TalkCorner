using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Common;
using TalkCorner.Domain.Entities;
using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Persistence.DatabaseContext;

public class TalkCornerDbContext(DbContextOptions<TalkCornerDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Board> Boards { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Thread> Threads { get; set; }
    public DbSet<User> Users { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var utcNow = DateTime.UtcNow;
        var entries = base.ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = utcNow;
                entry.Entity.Updated = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.Updated = utcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalkCornerDbContext).Assembly);

        // ========== User ==========
        // (z.B. DisplayName)
        modelBuilder.Entity<User>(builder =>
        {
            builder.OwnsOne(u => u.DisplayName, db =>
            {
                db.Property(d => d.Value)
                    .HasColumnName("DisplayName")
                    .IsRequired();
            });
        });

        // ========== Board ==========
        // Title und Description als ValueObject
        modelBuilder.Entity<Board>(builder =>
        {
            builder.OwnsOne(b => b.Title, tb =>
            {
                tb.Property(t => t.Value)
                    .HasColumnName("Title")
                    .IsRequired();
            });

            builder.OwnsOne(b => b.Description, db =>
            {
                db.Property(d => d.Value)
                    .HasColumnName("Description")
                    .IsRequired();
            });
        });

        // ========== Thread ==========
        // Title als ValueObject
        modelBuilder.Entity<Thread>(builder =>
        {
            builder.OwnsOne(t => t.Title, tb =>
            {
                tb.Property(ti => ti.Value)
                    .HasColumnName("Title")
                    .IsRequired();
            });
        });

        // ========== Post ==========
        // Content als ValueObject
        modelBuilder.Entity<Post>(builder =>
        {
            builder.OwnsOne(p => p.Content, cb =>
            {
                cb.Property(c => c.Value)
                    .HasColumnName("Content")
                    .IsRequired();
            });
        });
    }
}