using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TelephoneDirectory.Auth
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(new string[] { "https://localhost:44001/" });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
