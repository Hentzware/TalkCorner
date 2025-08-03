using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Persistence.DatabaseContext;
using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Persistence.Repositories;

/// <summary>
///     EF Core implementation of the thread repository.
/// </summary>
public class ThreadRepository(TalkCornerDbContext context) : GenericRepository<Thread>(context), IThreadRepository
{
    public async Task<Thread?> GetThreadByIdAsync(Guid id)
    {
        // Hole alle relevanten Relationen, z.B. Posts, Creator, Board, usw.
        return await context.Threads
            .AsNoTracking()
            .Include(x => x.Posts)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Thread?> GetThreadByIdWithTrackingAsync(Guid id)
    {
        return await context.Threads
            .Include(x => x.Posts)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Board)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Thread>> GetThreadsByBoardIdAsync(Guid boardId)
    {
        return await context.Threads
            .AsNoTracking()
            .Where(x => x.BoardId == boardId)
            .Include(x => x.Posts)
            .Include(x => x.CreatedByUser)
            .ToListAsync();
    }

    // Hier kannst du weitere Abfragen ergänzen, z. B. nach User, Paging etc.
}