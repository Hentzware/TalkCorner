using TalkCorner.Domain.Entities;

namespace TalkCorner.Application.Contracts.Persistence;

/// <summary>
///     Repository interface for <see cref="Board" /> entities.
/// </summary>
public interface IBoardRepository : IGenericRepository<Board>
{
    Task<IEnumerable<Board>> GetBoardsAsync();

    Task<Board?> GetBoardByIdAsync(Guid id);

    Task<Board?> GetBoardByIdWithTrackingAsync(Guid id);
}