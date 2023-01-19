using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TelephoneDirectory.Report.Consumers;

public static class ConsumerApiClient
{
    static public async Task<HttpResponseMessage> GetPersonUsingGuideApi(int id, Uri baseUri)
    {
        using (var client = new HttpClient { BaseAddress = baseUri })
        {
            try
            {
                var response = await client.GetAsync($"/api/provider?id=1");
                return response;
            }
            catch (System.Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }
        }
    }
}
