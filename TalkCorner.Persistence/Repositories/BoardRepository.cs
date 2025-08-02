using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

/// <summary>
///     EF Core implementation of the board repository.
/// </summary>
public class BoardRepository(TalkCornerDbContext context) : GenericRepository<Board>(context), IBoardRepository
{
    public async Task<IEnumerable<Board>> GetBoardsAsync()
    {
        return await context.Boards
            .AsNoTracking()
            .Include(x => x.CreatedByUser)
            .Include(x => x.Moderators)
            .Include(x => x.SubBoards)
            .Include(x => x.ParentBoard)
            .Include(x => x.Threads)
            .ToListAsync();
    }

    public async Task<Board?> GetBoardByIdAsync(Guid id)
    {
        return await context.Boards
            .AsNoTracking()
            .Include(x => x.CreatedByUser)
            .Include(x => x.Moderators)
            .Include(x => x.SubBoards)
            .Include(x => x.ParentBoard)
            .Include(x => x.Threads)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Board?> GetBoardByIdWithTrackingAsync(Guid id)
    {
        return await context.Boards
            .Include(x => x.CreatedByUser)
            .Include(x => x.Moderators)
            .Include(x => x.SubBoards)
            .Include(x => x.ParentBoard)
            .Include(x => x.Threads)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}