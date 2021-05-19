using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TelephoneDirectory.Guide
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
                    webBuilder.UseUrls("https://localhost:44337/");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
