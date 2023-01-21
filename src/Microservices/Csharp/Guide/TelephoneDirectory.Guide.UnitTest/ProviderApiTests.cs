using Microsoft.Extensions.Hosting;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace TelephoneDirectory.Guide.UnitTest;

public class ProviderApiTests : IClassFixture<ProviderApiTestsFixture>
{
    private ITestOutputHelper _outputHelper { get; }

    private readonly IHost server;
    public Uri ServerUri { get; }

    private readonly ProviderApiTestsFixture fixture;
    private readonly ITestOutputHelper _output;

    public ProviderApiTests(ProviderApiTestsFixture fixture, ITestOutputHelper output)
    {
        this.fixture = fixture;
        _output = output;
    }

    [Fact]
    public void EnsureProviderApiHonoursPactWithConsumer()
    {
        // Arrange
        var config = new PactVerifierConfig
        {
            Outputters = new List<IOutput>
            {
                // NOTE: PactNet defaults to a ConsoleOutput, however
                // xUnit 2 does not capture the console output, so this
                // sample creates a custom xUnit outputter. You will
                // have to do the same in xUnit projects.
                new XUnitOutput(_output),
            },
        };

        //Act / Assert
        IPactVerifier pactVerifier = new PactVerifier(config);
        var pactFile = new FileInfo(@"../../../../../pacts/consumer-provider.json");
        pactVerifier
            .ServiceProvider("Provide", fixture.ServerUri)
            .WithFileSource(pactFile)
            .WithProviderStateUrl(new Uri($"{fixture.PactServiceUri}/provider-states"))
            .Verify();
    }
}