using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Application.Contracts.Persistence;

public interface IThreadRepository : IGenericRepository<Thread>
{
    Task<Thread?> GetThreadByIdAsync(Guid id);

    Task<Thread?> GetThreadByIdWithTrackingAsync(Guid id);

    Task<IEnumerable<Thread>> GetThreadsByBoardIdAsync(Guid boardId);
}