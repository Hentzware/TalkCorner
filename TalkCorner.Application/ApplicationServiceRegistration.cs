using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TalkCorner.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly); });

        services.AddAutoMapper(config => { config.AddMaps(typeof(ApplicationServiceRegistration).Assembly); });

        return services;
    }
}