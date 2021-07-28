using System;
using System.Threading.Tasks;
using MassTransit;
using TelephoneDirectory.Contracts;

namespace TelephoneDirectory.Report.Consumers
{
    public class ReportCreatedConsumer : IConsumer<IGuideCreatedEvent>
    {
        public async Task Consume(ConsumeContext<IGuideCreatedEvent> context)
        {
            var reportId = context.Message.ReportId;
            await Console.Out.WriteLineAsync($"Report operation is succeeded! Report Id: {reportId}. Correlation Id: {context.Message.CorrelationId}");
            //Send mail, push notification, etc...

        }
    }
}
