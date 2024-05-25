using Serilog;

namespace ProductStore.Api.Configuration
{
    /// <summary>
    /// Custom configuration for Serilog injection
    /// </summary>
    public static class SerilogConfiguration
    {
        /// <summary>
        /// Allows to inject the necessary dependencies for Serilog configuration
        /// </summary>
        /// <param name="host">The host of the application</param>
        /// <param name="configuration">Configuration of the application</param>
        public static void Configure(this IHostBuilder host, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

            host.UseSerilog();
        }
    }
}
