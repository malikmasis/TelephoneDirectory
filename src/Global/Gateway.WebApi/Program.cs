using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Gateway.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .UseSerilog((_, config) =>
        {
            config
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console();
        })
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            config.AddOcelotWithSwaggerSupport(options =>
            {
                options.Folder = "OcelotConfiguration";
            });
        });
}
