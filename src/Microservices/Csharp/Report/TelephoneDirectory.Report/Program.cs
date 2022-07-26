using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using TelephoneDirectory.Report.Data;

namespace TelephoneDirectory.Report
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogging();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
                db.Database.Migrate();
            }
            host.Run();
        }

        private static void ConfigureLogging()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .Enrich.WithMachineName()
               .Enrich.WithProperty("Application", "Report")
               .WriteTo.Debug()
               .WriteTo.Console()
               .WriteTo.Elasticsearch(
                   new ElasticsearchSinkOptions(
                       new Uri(configuration["ElasticConfiguration:Uri"]))
                   {
                       AutoRegisterTemplate = true,
                       TemplateName = "serilog-events-template",
                       IndexFormat = "report-api-log-{0:yyyy.MM.dd}"
                   })
               .CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
    }
}
