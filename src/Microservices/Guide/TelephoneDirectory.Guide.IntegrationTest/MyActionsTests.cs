using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TelephoneDirectory.Guide.IntegrationTest
{
    public class MyActionsTests : BaseApiTests
    {
        public MyActionsTests() : base(null)
        {

        }

        [Fact]
        public async Task ShouldReturnWooHoo()
        {
            using var response = await HttpClient.GetAsync("/my-action");
            var body = await response.Content.ReadAsStringAsync();

            //response.StatusCode.ShouldBe(HttpStatusCodes.OK);
            //body.ShouldBe("woohoo!");
        }
    }
}
