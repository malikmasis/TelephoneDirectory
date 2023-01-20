using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TelephoneDirectory.Report.Consumers;
using Xunit;
using Xunit.Abstractions;

namespace TelephoneDirectory.Report.UnitTest
{
    public class ConsumerPactTests
    {
        private readonly IPactBuilderV3 pact;

        public ConsumerPactTests(ITestOutputHelper output)
        {
            var Config = new PactConfig
            {
                PactDir = @"..\..\..\..\..\pacts",
                Outputters = new[] { new XUnitOutput(output) },
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            pact = Pact.V3("Consumer", "Provider", Config).WithHttpInteractions();
        }

        [Fact]
        public async Task GetPerson_WhenIdGreaterThanZero_ReturnGuide()
        {
            int id = 1;

            pact.UponReceiving("A valid GET request for Guide")
                .Given("There is data")
                .WithRequest(HttpMethod.Get, "/api/guide/getperson/1")
                //.WithQuery("id", expectedDateString)
            .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithJsonBody(new { Id = id });

            // Act & Assert
            await pact.VerifyAsync(async ctx =>
            {
                var response = await ConsumerApiClient.GetPersonUsingGuideApi(id, ctx.MockServerUri);
                var body = await response.Content.ReadAsStringAsync();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                //Assert.Contains(expectedDateParsed, body);
            });
        }
    }
}
