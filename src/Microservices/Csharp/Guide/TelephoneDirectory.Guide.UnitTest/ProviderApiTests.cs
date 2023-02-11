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
    private readonly ProviderApiTestsFixture _fixture;
    private readonly ITestOutputHelper _output;

    public ProviderApiTests(ProviderApiTestsFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
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
                new XUnitOutput(_output),
            }
        };

        //Act / Assert
        IPactVerifier pactVerifier = new PactVerifier(config);
        var pactFile = new FileInfo(@"../../../../../../../Global/pacts/consumer-provider.json");
        pactVerifier
            .ServiceProvider("Provide", _fixture.ServerUri)
            .WithFileSource(pactFile)
            .WithProviderStateUrl(new Uri(_fixture.ServerUri, "provider-states"))
            .Verify();
    }
}