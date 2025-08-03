using TalkCorner.Domain.Entities;

namespace TalkCorner.Application.Contracts.Persistence;

/// <summary>
///     Repository-Interface für Posts.
/// </summary>
public interface IPostRepository : IGenericRepository<Post>
{
    // Beispiel für spätere Erweiterungen:
    Task<IEnumerable<Post>> GetPostsByThreadIdAsync(Guid threadId);
    Task<Post?> GetPostByIdWithTrackingAsync(Guid id);
}