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
        base.OnModelCreating(modelBuilder);
    }
}