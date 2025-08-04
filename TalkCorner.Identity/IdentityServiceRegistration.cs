using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Settings;
using TalkCorner.Identity.DatabaseContext;
using TalkCorner.Identity.Models;
using TalkCorner.Identity.Services;

namespace TalkCorner.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TalkCornerIdentityDb");
        var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();

        if (jwtSettings == null)
        {
            throw new NullReferenceException("JwtSettings is null.");
        }

        services.Configure<JwtSettings>(configuration.GetSection("JWT"));

        services.AddScoped<IAuthService, AuthService>();

        services.AddDbContext<TalkCornerIdentityDbContext>(builder => { builder.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 0))); });

        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TalkCornerIdentityDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey))
                };
            });

        return services;
    }
}