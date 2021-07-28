using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using TelephoneDirectory.Guide.StateMachines;

namespace TelephoneDirectory.Saga
{
    static class Program
    {
        public static async Task Main(string[] args)
        {
            var sagaStateMachine = new GuideStateMachine();
            var repository = new InMemorySagaRepository<GuideSagaState>();

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Durable = true;
                cfg.PrefetchCount = 1;
                cfg.PurgeOnStartup = true;
                cfg.Host(new Uri("rabbitmq://localhost"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
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
