using System;

namespace TelephoneDirectory.Contracts.Abstraction
{
    public interface IGuideCreatedEvent
    {
        Guid CorrelationId { get; }
        string ReportId { get; }
    }
}
