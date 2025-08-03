using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

/// <summary>
///     EF Core implementation of the post repository.
/// </summary>
public class PostRepository(TalkCornerDbContext context) : GenericRepository<Post>(context), IPostRepository
{
    public async Task<IEnumerable<Post>> GetPostsByThreadIdAsync(Guid threadId)
    {
        return await context.Posts
            .AsNoTracking()
            .Include(x => x.CreatedByUser)
            .Include(x => x.Thread)
            .Where(x => x.ThreadId == threadId)
            .ToListAsync();
    }

    public async Task<Post?> GetPostByIdWithTrackingAsync(Guid id)
    {
        return await context.Posts
            .Include(x => x.CreatedByUser)
            .Include(x => x.Thread)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}