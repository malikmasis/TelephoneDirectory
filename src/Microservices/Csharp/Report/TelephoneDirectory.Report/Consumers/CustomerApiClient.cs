using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TelephoneDirectory.Report.Consumers;

public static class ConsumerApiClient
{
    static public async Task<HttpResponseMessage> GetPersonUsingGuideApi(
        int id, 
        Uri baseUri,
        CancellationToken cancellationToken = default)
    {
        using (var client = new HttpClient { BaseAddress = baseUri })
        {
            try
            {
                return await client.GetAsync($"/api/guide/getperson/1", cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem connecting to Provider API.", ex);
            }
        }
    }
}
