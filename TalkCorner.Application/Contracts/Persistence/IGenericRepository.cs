using TalkCorner.Domain.Common;

namespace TalkCorner.Application.Contracts.Persistence;

/// <summary>
///     Generic repository interface for aggregate roots.
/// </summary>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Provides access to the Unit of Work for committing changes.
    /// </summary>
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    ///     Returns all entities of type <typeparamref name="T" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     Optional token that can be used to cancel the asynchronous operation.
    /// </param>
    /// <returns>A collection of all entities of type <typeparamref name="T" />.</returns>
    Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <param name="cancellationToken">
    ///     Optional token that can be used to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    ///     The entity matching the specified identifier, or <c>null</c> if none is found.
    /// </returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<T?> GetByIdWithTrackingAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a new entity to the repository. Changes are only persisted when
    ///     <see cref="IUnitOfWork.SaveChangesAsync" /> is called.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">
    ///     Optional token that can be used to cancel the asynchronous operation.
    /// </param>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Marks an existing entity as updated. Changes are only persisted when
    ///     <see cref="IUnitOfWork.SaveChangesAsync" /> is called.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">
    ///     Optional token that can be used to cancel the asynchronous operation.
    /// </param>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes an entity identified by its unique identifier. Changes are only persisted when
    ///     <see cref="IUnitOfWork.SaveChangesAsync" /> is called.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <param name="cancellationToken">
    ///     Optional token that can be used to cancel the asynchronous operation.
    /// </param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes the specified entity. Changes are only persisted when
    ///     <see cref="IUnitOfWork.SaveChangesAsync" /> is called.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">
    ///     Optional token that can be used to cancel the asynchronous operation.
    /// </param>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}