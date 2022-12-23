using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Xunit;

namespace TelephoneDirectory.Report.IntegrationTest.Base;

public class BaseTest : IClassFixture<TestAuthFactory<TestStartup>>
{
    protected readonly WebApplicationFactory<TestStartup> _factory;

    public BaseTest(TestAuthFactory<TestStartup> factory)
    {
        var projectDir = Directory.GetCurrentDirectory();
        var configPath = Path.Combine(projectDir, "appsettings.json");

        _factory = factory.WithWebHostBuilder(builder =>
         {
             builder.ConfigureAppConfiguration((context, conf) =>
             {
                 conf.AddJsonFile(configPath);
             });

             builder.UseSolutionRelativeContentRoot("");

             builder.ConfigureTestServices(services =>
             {
                 services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
             });
         });
    }
}
