using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace TelephoneDirectory.Guide.UnitTest;

public class ProviderApiTests
{
    private readonly string _pactServiceUri = "http://127.0.0.1:9001";
    private ITestOutputHelper _outputHelper { get; }

    public ProviderApiTests(ITestOutputHelper output)
    {
        _outputHelper = output;
    }

    [Fact]
    public void EnsureProviderApiHonoursPactWithConsumer()
    {
        // Arrange
        var config = new PactVerifierConfig
        {
            // NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output,
            // so a custom outputter is required.
            Outputters = new List<IOutput>
                {
                    new XUnitOutput(_outputHelper)
                }
        };

        using (var _webHost = WebHost.CreateDefaultBuilder()
            .UseStartup<TestStartup>()
            .UseUrls(_pactServiceUri).Build())
        {
            _webHost.Start();

            //Act / Assert
            IPactVerifier pactVerifier = new PactVerifier(config);
            var pactFile = new FileInfo(@"../../../../../pacts/consumer-provider.json");
            pactVerifier
                .ServiceProvider("Provide", new Uri(_pactServiceUri))
                .WithFileSource(pactFile)
                .WithProviderStateUrl(new Uri($"{_pactServiceUri}/provider-states"))
                .Verify();
        }
    }
}