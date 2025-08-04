using TalkCorner.Application.Features.Authentication.Common;

namespace TalkCorner.Application.Contracts.Identity;

public interface IApplicationUserRepository
{
    Task<bool> EmailExistsAsync(string email);

    Task<IEnumerable<ApplicationUserDto>> GetAllApplicationUsersAsync();

    Task<ApplicationUserDto?> GetApplicationUserByIdAsync(Guid id);
}