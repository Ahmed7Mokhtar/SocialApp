using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seed
{
    internal class DatabaseInitializationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseInitializationHostedService> _logger;
        private readonly IHostEnvironment _env;

        public DatabaseInitializationHostedService(IServiceProvider serviceProvider, ILogger<DatabaseInitializationHostedService> logger, IHostEnvironment env)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _env = env;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if(!_env.IsDevelopment())
                return;

            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                _logger.LogInformation("Applying database migrations...");
                await context.Database.MigrateAsync(cancellationToken);

                _logger.LogInformation("Seeding database...");
                await Seed.SeedUsers(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
