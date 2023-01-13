using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Guide.UnitTest;

public class ProviderStateMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDictionary<string, Action> _providerStates;

    public ProviderStateMiddleware(RequestDelegate next)
    {
        _next = next;
        _providerStates = new Dictionary<string, Action>
        {
            {
                "There is no data",
                RemoveAllData
            },
            {
                "There is data",
                AddData
            }
        };
    }

    private void RemoveAllData()
    {
        var deletePath = Path.Combine(DataPath(), "somedata.txt");

        if (File.Exists(deletePath))
        {
            File.Delete(deletePath);
        }
    }

    private void AddData()
    {
        var writePath = Path.Combine(DataPath(), "somedata.txt");

        if (!Directory.Exists(DataPath()))
        {
            Directory.CreateDirectory(DataPath());
        }

        if (!File.Exists(writePath))
        {
            File.Create(writePath);
        }
    }

    private string DataPath()
    {
        return Path.Combine(Path.GetTempPath(), "data");
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/provider-states"))
        {
            await this.HandleProviderStatesRequestAsync(context);
            await context.Response.WriteAsync(String.Empty);
        }
        else
        {
            await this._next(context);
        }
    }

    private async Task HandleProviderStatesRequestAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
            context.Request.Body != null)
        {
            string jsonRequestBody = String.Empty;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                jsonRequestBody = await reader.ReadToEndAsync();
            }

            var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

            //A null or empty provider state key must be handled
            if (providerState != null && !String.IsNullOrEmpty(providerState.State))
            {
                _providerStates[providerState.State].Invoke();
            }
        }
    }
}
