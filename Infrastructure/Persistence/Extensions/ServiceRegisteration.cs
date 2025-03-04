using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Seed;

namespace Persistence.Extensions;

public static class ServiceRegisteration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opts => 
        {   
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddHostedService<DatabaseInitializationHostedService>();

        return services;
    }
}
