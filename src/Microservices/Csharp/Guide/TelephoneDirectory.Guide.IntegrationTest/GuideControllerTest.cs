using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TelephoneDirectory.Guide.Entities;
using Xunit;

namespace TelephoneDirectory.Guide.IntegrationTest
{
    public class GuideControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public GuideControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_guide_by_id()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Guide/get/1");
            Assert.True(response.IsSuccessStatusCode);

            var body = await response.Content.ReadAsStringAsync();
            var reply = JsonSerializer.Deserialize<Person>(body);
            Assert.NotNull(reply);
        }
    }
}
