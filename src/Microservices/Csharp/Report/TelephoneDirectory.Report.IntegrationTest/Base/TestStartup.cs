using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TelephoneDirectory.Report.IntegrationTest.Base;

public sealed class TestStartup : Startup
{
    public TestStartup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = TestAuthHandler.DefaultScheme;
            options.DefaultScheme = TestAuthHandler.DefaultScheme;
        })
        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
            TestAuthHandler.DefaultScheme, options => { });
    }
}
