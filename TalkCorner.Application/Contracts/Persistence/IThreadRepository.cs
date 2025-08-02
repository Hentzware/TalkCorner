using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Application.Contracts.Persistence;

public interface IThreadRepository : IGenericRepository<Thread>
{
}