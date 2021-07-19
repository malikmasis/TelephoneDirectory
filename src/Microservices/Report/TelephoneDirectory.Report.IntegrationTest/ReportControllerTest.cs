using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelephoneDirectory.Report.Entities;
using TelephoneDirectory.Report.IntegrationTest.Base;
using Xunit;

namespace TelephoneDirectory.Report.IntegrationTest
{
    public class ReportControllerTest : IClassFixture<MediaGalleryFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;

        public ReportControllerTest(MediaGalleryFactory<TestStartup> factory)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("");

                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }

        [Fact]
        public async Task Get_all_reports()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Report/getall");
            Assert.True(response.IsSuccessStatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var reply = JsonSerializer.Deserialize<List<ReportOutput>>(body);
            Assert.NotNull(reply);
        }

        [Fact]
        public async Task Get_report_by_id()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Report/get/1");
            Assert.True(response.IsSuccessStatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var reply = JsonSerializer.Deserialize<ReportOutput>(body);
            Assert.NotNull(reply);
        }
    }

}
