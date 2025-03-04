using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace API.Extensions
{
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(builder.Configuration) // Read settings from appsettings.json
            //    .Enrich.FromLogContext() // Add contextual information to logs
            //    .CreateLogger();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName() // Add machine name to logs
                .Enrich.WithThreadId() // Add thread ID to logs
                .WriteTo.Async(m => m.Console())
                .WriteTo.Async(m => m.File(
                    path: "Logs/log.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    formatter: new CompactJsonFormatter()
                ))
                .CreateLogger();

            builder.Host.UseSerilog();

            return builder;
        }
    }
}
