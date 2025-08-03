using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Persistence.DatabaseContext;
using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Persistence.Repositories;

public class ThreadRepository(TalkCornerDbContext context) : GenericRepository<Thread>(context), IThreadRepository
{
}