using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IBrewerService, BrewerService>();
        
        return services;
    }
}