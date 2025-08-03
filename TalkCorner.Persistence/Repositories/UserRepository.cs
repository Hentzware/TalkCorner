using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

public class UserRepository(TalkCornerDbContext context) : GenericRepository<User>(context), IUserRepository
{
}