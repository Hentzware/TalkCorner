using Microsoft.AspNetCore.Identity;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Identity.Models;

namespace TalkCorner.Identity.Repositories;

public class ApplicationUserRepository(UserManager<ApplicationUser> userManager) : IApplicationUserRepository
{
    public async Task<bool> EmailExistsAsync(string email)
    {
        var user = await userManager.FindByNameAsync(email);
        return user != null;
    }
}