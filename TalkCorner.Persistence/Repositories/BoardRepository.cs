using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

/// <summary>
///     EF Core implementation of the board repository.
/// </summary>
public class BoardRepository(TalkCornerDbContext context) : GenericRepository<Board>(context), IBoardRepository
{
}