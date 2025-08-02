using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Persistence.DatabaseContext;
using TalkCorner.Persistence.Repositories;

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
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 0)), sqlOptions => sqlOptions.EnableRetryOnFailure());
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
}