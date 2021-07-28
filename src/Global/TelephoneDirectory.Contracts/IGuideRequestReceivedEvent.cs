using System;

namespace TelephoneDirectory.Contracts
{
    public interface IGuideRequestReceivedEvent
    {
        Guid CorrelationId { get; }
        string ReportId { get; }
    }
}
