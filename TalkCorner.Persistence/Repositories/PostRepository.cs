using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

public class PostRepository(TalkCornerDbContext context) : GenericRepository<Post>(context), IPostRepository
{
}