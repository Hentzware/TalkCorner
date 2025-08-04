using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Features.Authentication.Common;
using TalkCorner.Identity.Models;

namespace TalkCorner.Identity.Repositories;

public class ApplicationUserRepository(UserManager<ApplicationUser> userManager) : IApplicationUserRepository
{
    public async Task<bool> EmailExistsAsync(string email)
    {
        var user = await userManager.FindByNameAsync(email);
        return user != null;
    }

    public async Task<IEnumerable<ApplicationUserDto>> GetAllApplicationUsersAsync()
    {
        return await userManager.Users
            .Select(x => new ApplicationUserDto()
            {
                Id = Guid.Parse(x.Id),
                Email = x.Email!
            })
            .ToListAsync();
    }

    public async Task<ApplicationUserDto?> GetApplicationUserByIdAsync(Guid id)
    {
        return await userManager.Users
            .Where(x => x.Id == id.ToString())
            .Select(x => new ApplicationUserDto()
            {
                Id = Guid.Parse(x.Id),
                Email = x.Email!
            })
            .FirstOrDefaultAsync();
    }
}