using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TalkCorner.Identity.Models;

namespace TalkCorner.Identity.DatabaseContext;

public class TalkCornerIdentityDbContext(DbContextOptions<TalkCornerIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(TalkCornerIdentityDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}