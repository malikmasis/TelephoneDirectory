using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Testing;
using Moq;
using TelephoneDirectory.Guide.Entities;
using TelephoneDirectory.Report.Consumers;
using TelephoneDirectory.Report.Interfaces;
using Xunit;

namespace TelephoneDirectory.Report.UnitTest
{
    public class TestPersonConsumer
    {
        [Fact]
        public async Task Should_test_the_consumer()
        {

            var mockedReportService = new Mock<IReportService>();
            var token = new CancellationTokenSource().Token;
            mockedReportService.Setup(rs => rs.Save(token)).Returns(Task.CompletedTask);

            var harness = new InMemoryTestHarness();
            var consumer = harness.Consumer<PersonConsumer>(() => new PersonConsumer(mockedReportService.Object), null);

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new Person() { Name = "Hi" });

                // did the endpoint consume the message
                Assert.True(harness.Consumed.Select<Person>().Any());

                // did the actual consumer consume the message
                Assert.True(consumer.Consumed.Select<Person>().Any());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
