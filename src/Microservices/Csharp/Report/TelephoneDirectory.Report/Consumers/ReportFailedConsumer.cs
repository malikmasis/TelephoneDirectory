using MassTransit;
using System;
using System.Threading.Tasks;
using TelephoneDirectory.Contracts.Abstraction;

namespace TelephoneDirectory.Report.Consumers;

public class ReportFailedConsumer : IConsumer<IGuideFailedEvent>
{
    public async Task Consume(ConsumeContext<IGuideFailedEvent> context)
    {
        var reportId = context.Message.ReportId;
        await Console.Out.WriteLineAsync($"Report operation is failed! Report Id: {reportId}. Fault Message. Correlation Id: {context.Message.CorrelationId}");
        //Send mail, push notification, etc...
    }
}
