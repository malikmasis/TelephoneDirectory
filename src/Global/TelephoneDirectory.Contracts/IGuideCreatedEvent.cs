using System;

namespace TelephoneDirectory.Contracts
{
    public interface IGuideCreatedEvent
    {
        Guid CorrelationId { get; }
        string ReportId { get; }
    }
}
