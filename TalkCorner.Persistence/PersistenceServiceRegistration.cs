using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Domain.Entities;
using TalkCorner.Persistence.DatabaseContext;
using TalkCorner.Persistence.Repositories;
using Thread = TalkCorner.Domain.Entities.Thread;

namespace TalkCorner.Persistence;

/// <summary>
///     Registers the persistence layer (DbContext and repositories) in the DI container,
///     validates the connection string format, and verifies database connectivity at app startup.
/// </summary>
public static class PersistenceServiceRegistration
{
    /// <summary>
    ///     Adds all persistence-related services:
    ///     1. Reads the "TalkCornerDb" connection string and throws if missing.
    ///     2. Validates its syntax using DbConnectionStringBuilder.
    ///     3. Registers the TalkCornerDbContext using MySQL provider and retry policy.
    ///     4. Registers IGenericRepository and IBoardRepository.
    ///     5. Tests database connectivity via Database.CanConnect().
    /// </summary>
    /// <param name="services">The IServiceCollection to extend</param>
    /// <param name="configuration">The application configuration (e.g. appsettings.json)</param>
    /// <returns>The extended IServiceCollection.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the connection string is missing or if the database is unreachable.
    /// </exception>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Retrieve the connection string from configuration (e.g. appsettings.json)
        var connectionString = configuration.GetConnectionString("TalkCornerDb")
                               ?? throw new InvalidOperationException("ConnectionString 'TalkCornerDb' not found.");

        // 2. Syntax validation: DbConnectionStringBuilder throws if format is invalid
        _ = new DbConnectionStringBuilder { ConnectionString = connectionString };

        // 3. Register DbContext with MySQL provider and retry policy on failure
        services.AddDbContext<TalkCornerDbContext>(options =>
        {
            options
                .UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 0)), sqlOptions => sqlOptions.EnableRetryOnFailure())
                .UseSeeding((context, _) => SeedDomainData(context))
                .UseAsyncSeeding(async (context, _, ct) => await SeedDomainDataAsync(context, ct));
        });

        // 4. Register repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBoardRepository, BoardRepository>();

        // 5. Immediately validate database connectivity at startup
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TalkCornerDbContext>();

        if (!dbContext.Database.CanConnect())
        {
            throw new InvalidOperationException("Connection to TalkCornerDb cannot be established.");
        }

        return services;
    }

    private static void SeedDomainData(DbContext context)
    {
        var ctx = (TalkCornerDbContext)context;
        Console.WriteLine("Seeding start…");

        if (ctx.Users.AsEnumerable().Any(u => u.DisplayName.Value == "User01"))
        {
            Console.WriteLine("Seed skipped: User exists");
            return;
        }

        var user = User.Create(Guid.NewGuid(), "User01");
        var board = Board.Create("Board 1", "Desc", user);
        var thread = Thread.Create("Test Thread", user, board);
        var post = Post.Create("Hallo Welt", user, thread);

        ctx.Users.Add(user);
        ctx.SaveChanges();
    }

    private static async Task SeedDomainDataAsync(DbContext context, CancellationToken ct)
    {
        await Task.Run(() => SeedDomainData(context), ct);
    }
}