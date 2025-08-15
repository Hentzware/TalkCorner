using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TalkCorner.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "C99AC715-51B2-400E-9625-290D19418B70",
                RoleId = "6D82E62C-559F-4489-B6B3-BDA2D3877D41"
            },
            new IdentityUserRole<string>
            {
                UserId = "4895AD73-D889-49DE-9418-E8B163BEACB5",
                RoleId = "8102AD91-84CB-423E-8DCA-AD2839D8149D"
            }
        );
    }
}