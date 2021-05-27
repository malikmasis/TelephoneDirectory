using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace TelephoneDirectory.Auth
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((host, log) =>
                {
                    if (host.HostingEnvironment.IsProduction())
                        log.MinimumLevel.Information();
                    else
                        log.MinimumLevel.Debug();

                    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
                    log.WriteTo.Console();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(new string[] { "https://localhost:44001/" });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
