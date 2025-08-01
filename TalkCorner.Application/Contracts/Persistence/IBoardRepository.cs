using TalkCorner.Domain.Entities;

namespace TalkCorner.Application.Contracts.Persistence;

/// <summary>
///     Repository interface for <see cref="Board" /> entities.
/// </summary>
public interface IBoardRepository : IGenericRepository<Board>
{
}