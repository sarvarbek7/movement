using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Infrastructure.Network.Apis.VirtualOffice;

namespace Movement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register infrastructure services, e.g., database context, repositories, etc.
        services.AddHttpClient<IVirtualOfficeHttpService, VirtualOfficeHttpService>("virtual-office-http-client",client =>
        {
            client.BaseAddress = new Uri(configuration["VirtualOfficeApiSettings:BaseUrl"] 
                ?? throw new InvalidOperationException("VirtualOfficeApiSettings:BaseUrl configuration is missing."));
        })
        // .AddExtendedHttpClientLogging()
        .AddStandardResilienceHandler();

        return services;
    }

}