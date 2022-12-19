using System;

namespace TelephoneDirectory.Contracts.Abstraction
{
    public interface IGuideFailedEvent
    {
        Guid CorrelationId { get; }
        string ReportId { get; }
    }
}
