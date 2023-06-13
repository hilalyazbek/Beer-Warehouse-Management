using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterates.Bwm.Infrastructure.Repositories;
using WatchDog;
using WatchDog.src.Enums;
using Iterates.Bwm.Domain.Interfaces.Repositories;
using Iterates.Bwm.Domain.Interfaces.Logging;
using Iterates.Bwm.Infrastructure.Logging;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");

        // Add MSSQL DB Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(conn));

        services.AddScoped<IGenericRepository<Brewer>, GenericRepository<Brewer>>();
        services.AddScoped<IGenericRepository<Beer>, GenericRepository<Beer>>();
        services.AddScoped<IGenericRepository<Wholesaler>, GenericRepository<Wholesaler>>();
        services.AddScoped<IGenericRepository<WholesalerStock>, GenericRepository<WholesalerStock>>();
        services.AddScoped<IGenericRepository<Sale>, GenericRepository<Sale>>();

        return services;
    }

    public static void AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
    {

        var conn = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<ILoggerManager, LoggerManager>();
        services.AddWatchDogServices();
    }
}