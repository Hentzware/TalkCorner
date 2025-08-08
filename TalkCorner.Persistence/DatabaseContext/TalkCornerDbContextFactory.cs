using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TalkCorner.Persistence.DatabaseContext;

public class TalkCornerDbContextFactory : IDesignTimeDbContextFactory<TalkCornerDbContext>
{
    public TalkCornerDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TalkCornerDbContext>();
        var connectionString = config.GetConnectionString("TalkCornerDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("ConnectionString TalkCornerDb not found.");
        }

        optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(9, 4, 0)));

        return new TalkCornerDbContext(optionsBuilder.Options);
    }
}