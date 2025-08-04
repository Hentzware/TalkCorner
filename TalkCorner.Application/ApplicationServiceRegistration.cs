using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalkCorner.Application.Behaviors;

namespace TalkCorner.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly); });

        services.AddAutoMapper(config => { config.AddMaps(typeof(ApplicationServiceRegistration).Assembly); });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

        return services;
    }
}