using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Common;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

/// <summary>
/// Generic EF Core repository implementation for aggregate roots.
/// </summary>
/// <typeparam name="T">The aggregate root type.</typeparam>
/// <param name="context">The database context injected via primary constructor.</param>
public class GenericRepository<T>(TalkCornerDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    /// <inheritdoc/>
    public IUnitOfWork UnitOfWork => context;

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<T?> GetByIdWithTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context
            .Set<T>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await context
            .Set<T>()
            .AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            context.Set<T>().Remove(entity);
        }
    }

    /// <inheritdoc/>
    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}
