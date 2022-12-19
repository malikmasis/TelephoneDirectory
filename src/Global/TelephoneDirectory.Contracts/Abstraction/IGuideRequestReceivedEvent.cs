using System;

namespace TelephoneDirectory.Contracts.Abstraction
{
    public interface IGuideRequestReceivedEvent
    {
        Guid CorrelationId { get; }
        string ReportId { get; }
    }
}
