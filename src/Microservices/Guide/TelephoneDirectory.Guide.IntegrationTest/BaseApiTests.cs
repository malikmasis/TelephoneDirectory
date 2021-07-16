using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace TelephoneDirectory.Guide.IntegrationTest
{
    public abstract class BaseApiTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        protected readonly WebApplicationFactory<Startup> _factory;

        protected BaseApiTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        protected HttpClient HttpClient;
        protected ApiFactory ApplicationFactory;
        private ConnectHandle sendObserverHandle;

        public async Task InitializeAsync()
        {
            ApplicationFactory = new ApiFactory();

            //TestSendObserver = new TestSendObserver();

            HttpClient = _factory.CreateClient();
            sendObserverHandle = ApplicationFactory.Server.Host.Services.GetRequiredService<IBus>().ConnectSendObserver(null);
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            ApplicationFactory.Dispose();
            await Task.CompletedTask;
        }
    }
}
