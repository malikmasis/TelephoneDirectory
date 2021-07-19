using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TelephoneDirectory.Report.Entities;
using Xunit;

namespace TelephoneDirectory.Report.IntegrationTest
{
    public class ReportControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public ReportControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
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
    }
}
