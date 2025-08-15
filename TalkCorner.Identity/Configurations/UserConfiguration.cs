using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalkCorner.Identity.Models;

namespace TalkCorner.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser()
            {
                Id = "C99AC715-51B2-400E-9625-290D19418B70",
                Email = "admin@localhost.de",
                NormalizedEmail = "ADMIN@LOCALHOST.DE",
                UserName = "admin@localhost.de",
                NormalizedUserName = "ADMIN@LOCALHOST.DE",
                PasswordHash = passwordHasher.HashPassword(null, "Passw0rd"),
                EmailConfirmed = true
            },
            new ApplicationUser()
            {
                Id = "4895AD73-D889-49DE-9418-E8B163BEACB5",
                Email = "user@localhost.de",
                NormalizedEmail = "USER@LOCALHOST.DE",
                UserName = "user@localhost.de",
                NormalizedUserName = "USER@LOCALHOST.DE",
                PasswordHash = passwordHasher.HashPassword(null, "Passw0rd"),
                EmailConfirmed = true
            }
        );
    }
}