using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Application.Services;
using WatchDog;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IBrewerService, BrewerService>();
        services.AddScoped<IWholesalerService, WholesalerService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }

    public static IApplicationBuilder AddWatchDog(this IApplicationBuilder app)
    {
        app.UseWatchDogExceptionLogger();
        app.UseWatchDog(options =>
        {
            options.WatchPageUsername = "admin";
            options.WatchPagePassword = "admin";
        });

        return app;
    }
}