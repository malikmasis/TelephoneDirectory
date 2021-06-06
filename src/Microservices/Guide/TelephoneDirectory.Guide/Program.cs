using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using TelephoneDirectory.Guide.Data;

namespace TelephoneDirectory.Guide
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GuideDbContext>();
                db.Database.Migrate();
            }
            host.Run();
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
                    //webBuilder.UseUrls(new string[] { "http://localhost:44337/" });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
