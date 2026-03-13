using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Infrastructure.Network.Apis.VirtualOffice;
using Movement.Application.Behaviours;
using Movement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Movement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MovementDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddRedaction();

        // Register infrastructure services, e.g., database context, repositories, etc.
        services.AddHttpClient<IVirtualOfficeHttpService, VirtualOfficeHttpService>("virtual-office-http-client", client =>
        {
            client.BaseAddress = new Uri(configuration["VirtualOfficeApiSettings:BaseUrl"]
                ?? throw new InvalidOperationException("VirtualOfficeApiSettings:BaseUrl configuration is missing."));
        })
        .AddExtendedHttpClientLogging(c =>
        {
            c.LogRequestStart = true;
        })
        .AddStandardResilienceHandler();


        services.AddMediator(options =>
        {
            options.Assemblies =
            [
                typeof(Application.DependencyInjection).Assembly, // Application layer assembly
                typeof(DependencyInjection).Assembly // Infrastructure layer assembly
            ];


            options.NotificationPublisherType = typeof(Mediator.TaskWhenAllPublisher);

            options.ServiceLifetime = ServiceLifetime.Scoped;

            options.PipelineBehaviors = [
                typeof(CheckPinfQueryBehaviour<,>),
                typeof(CheckPinfCommandBehaviour<,>),

                typeof(CheckShiftQueryBehaviour<,>),
                typeof(CheckShiftCommandBehaviour<,>)
            ];
        });

        return services;
    }

}