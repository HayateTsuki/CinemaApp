using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Cinema.Core.Extensions
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseConfiguredLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureLogging(logging => { logging.ClearProviders(); })
                .UseSerilog();
            return hostBuilder;
        }

        public static void RunLogged(this IHost host)
        {
            try
            {
                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Stopped program because of exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static async Task RunLoggedAsync(this IHost host)
        {
            try
            {
                await host.RunAsync();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Stopped program because of exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static void AddConfiguredLogger(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
