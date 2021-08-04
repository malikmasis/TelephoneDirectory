using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using Microsoft.Extensions.Configuration;
using TelephoneDirectory.Guide.StateMachines;

namespace TelephoneDirectory.Saga
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                        .Build();

            var sagaStateMachine = new GuideStateMachine();
            var repository = new InMemorySagaRepository<GuideSagaState>();

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Durable = true;
                cfg.PrefetchCount = 1;
                cfg.PurgeOnStartup = true;
                cfg.Host(new Uri(configuration["Rabbitmq:Url"]), hst =>
                {
                    hst.Username(configuration["Rabbitmq:Username"]);
                    hst.Password(configuration["Rabbitmq:Password"]);
                });

                cfg.ReceiveEndpoint("saga.service", e =>
                {
                    e.StateMachineSaga(sagaStateMachine, repository);
                });
            });
            await bus.StartAsync(CancellationToken.None);
            Console.WriteLine("Saga active.. Press enter to exit");
            Console.ReadLine();
            await bus.StopAsync(CancellationToken.None);
        }
    }
}
