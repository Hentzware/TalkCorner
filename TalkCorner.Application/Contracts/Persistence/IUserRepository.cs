using TalkCorner.Domain.Entities;

namespace TalkCorner.Application.Contracts.Persistence;

/// <summary>
///     Repository-Interface für Benutzer.
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    // Beispiele für mögliche Erweiterungen:
    Task<User?> GetUserByUsernameAsync(string username);
}