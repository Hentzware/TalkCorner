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
        var users = await userManager.Users.ToListAsync();
        var results = new List<ApplicationUserDto>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);

            results.Add(new ApplicationUserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email!,
                Roles = roles.ToList()
            });
        }

        return results;
    }

    public async Task<ApplicationUserDto?> GetApplicationUserByIdAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return null;
        }

        var roles = await userManager.GetRolesAsync(user);

        return new ApplicationUserDto
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email!,
            Roles = roles.ToList()
        };
    }
}