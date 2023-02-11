using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace TelephoneDirectory.Guide.UnitTest;

public class ProviderApiTestsFixture : IDisposable
{
    public Uri ServerUri { get; }

    private const string _pactServiceUri = "http://localhost:44337";

    private readonly IHost _server;

    public ProviderApiTestsFixture()
    {
        ServerUri = new Uri(_pactServiceUri);
        _server = Host.CreateDefaultBuilder()
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         webBuilder.UseUrls(ServerUri.ToString());
                         webBuilder.UseStartup<TestStartup>();
                     })
                     .Build();
        _server.Start();
    }

    public void Dispose()
    {
        _server.Dispose();
    }
}