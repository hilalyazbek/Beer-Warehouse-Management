using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces;
using Iterates.Bwm.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iterates.Bwm.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection");
        // Add Postgres DB Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(conn));
        
        services.AddScoped<IGenericRepository<Brewer>, GenericRepository<Brewer>>();
        services.AddScoped<IGenericRepository<Beer>, GenericRepository<Beer>>();
        services.AddTransient<IGenericRepository<Wholesaler>, GenericRepository<Wholesaler>>();
        services.AddTransient<IGenericRepository<WholesalerStock>, GenericRepository<WholesalerStock>>();
        services.AddScoped<IGenericRepository<Sale>, GenericRepository<Sale>>();

        return services;
    }
}