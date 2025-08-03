using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;

namespace TalkCorner.Persistence.Repositories;

/// <summary>
///     EF Core implementation of the user repository.
/// </summary>
public class UserRepository(TalkCornerDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.DisplayName.Value == username);
    }
}