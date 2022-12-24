using MassTransit;
using System;
using System.Threading.Tasks;
using TelephoneDirectory.Contracts.Abstraction;

namespace TelephoneDirectory.Report.Consumers;

public sealed class ReportRequestReceivedConsumer : IConsumer<IGuideRequestReceivedEvent>
{
    public async Task Consume(ConsumeContext<IGuideRequestReceivedEvent> context)
    {
        var reportId = context.Message.ReportId;
        await Console.Out.WriteLineAsync($"Report request is received, report id is; {reportId}. Correlation Id: {context.Message.CorrelationId}");
        //Get report from Db, file, etc...
        if (reportId.StartsWith("report-", StringComparison.Ordinal))
        {
            await context.Publish<IGuideCreatedEvent>(new
            {
                context.Message.CorrelationId,
                context.Message.ReportId,
                CreationTime = DateTime.Now
            });
        }
        else
        {
            await context.Publish<IGuideFailedEvent>(new
            {
                context.Message.CorrelationId,
                context.Message.ReportId,
                FaultMessage = "Report name is invalid! Please retry again!",
                FaultTime = DateTime.Now
            });
        }
    }
}
