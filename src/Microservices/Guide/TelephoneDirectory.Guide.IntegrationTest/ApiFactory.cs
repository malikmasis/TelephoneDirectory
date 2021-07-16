using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace TelephoneDirectory.Guide.IntegrationTest
{
    public class ApiFactory : WebApplicationFactory<TelephoneDirectory.Guide.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
           
        }
    }
}
