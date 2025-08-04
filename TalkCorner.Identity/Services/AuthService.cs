using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Features.Authentication;
using TalkCorner.Application.Features.Authentication.Login;
using TalkCorner.Application.Features.Authentication.Register;
using TalkCorner.Application.Settings;
using TalkCorner.Domain.Entities;
using TalkCorner.Identity.Models;

namespace TalkCorner.Identity.Services;

public class AuthService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) : IAuthService
{
    public async Task<AuthenticationResponse> LoginAsync(LoginCommand request)
    {
        var applicationUser = await userManager.FindByEmailAsync(request.Email);

        if (applicationUser == null)
        {
            throw new Exception();
        }

        var result = await signInManager.CheckPasswordSignInAsync(applicationUser, request.Password, false);

        if (!result.Succeeded)
        {
            throw new Exception();
        }

        var user = (await userRepository.GetAsync()).FirstOrDefault(x => x.ApplicationUserId == Guid.Parse(applicationUser.Id));

        if (user == null)
        {
            throw new Exception();
        }

        var jwtSecurityToken = await GenerateTokenAsync(applicationUser, user);

        return new AuthenticationResponse { Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) };
    }

    public async Task<AuthenticationResponse> RegisterAsync(RegisterCommand request)
    {
        var applicationUser = await userManager.FindByEmailAsync(request.Email);

        if (applicationUser != null)
        {
            throw new Exception();
        }

        applicationUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            UserName = request.Email,
            NormalizedUserName = request.Email.ToUpper(),
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(applicationUser, request.Password);

        if (!result.Succeeded)
        {
            throw new Exception();
        }

        await userManager.AddToRoleAsync(applicationUser, "User");

        var user = User.Create(Guid.Parse(applicationUser.Id), request.DisplayName);

        await userRepository.AddAsync(user);
        await userRepository.UnitOfWork.SaveChangesAsync();

        var jwtSecurityToken = await GenerateTokenAsync(applicationUser, user);

        return new AuthenticationResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };
    }

    private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser applicationUser, User user)
    {
        var roles = (await userManager.GetRolesAsync(applicationUser)).Select(x => new Claim(ClaimTypes.Role, x)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(roles);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.IssuerSigningKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            audience: jwtSettings.Value.Audience,
            issuer: jwtSettings.Value.Issuer,
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Value.Expiration));

        return jwtSecurityToken;
    }
}