using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TalkCorner.Identity.DatabaseContext;

public class TalkCornerIdentityDbContextFactory : IDesignTimeDbContextFactory<TalkCornerIdentityDbContext>
{
    public TalkCornerIdentityDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TalkCornerIdentityDbContext>();
        var connectionString = config.GetConnectionString("TalkCornerIdentityDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("ConnectionString TalkCornerIdentityDb not found.");
        }

        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 0)));

        return new TalkCornerIdentityDbContext(optionsBuilder.Options);
    }
}