using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TelephoneDirectory.Guide.UnitTest;

public class ProviderApiTestsFixture : IDisposable
{
    private const string _pactServiceUri = "http://localhost:44337";
    private readonly IHost server;
    public Uri ServerUri { get; }

    public ProviderApiTestsFixture()
    {
        ServerUri = new Uri(_pactServiceUri);
        server = Host.CreateDefaultBuilder()
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         webBuilder.UseUrls(ServerUri.ToString());
                         webBuilder.UseStartup<TestStartup>();
                     })
                     .Build();
        server.Start();
    }

    public void Dispose()
    {
        server.Dispose();
    }
}