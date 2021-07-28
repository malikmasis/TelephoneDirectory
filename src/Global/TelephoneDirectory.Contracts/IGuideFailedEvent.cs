using System;

namespace TelephoneDirectory.Contracts
{
    public interface IGuideFailedEvent
    {
        Guid CorrelationId { get; }
        string ReportId { get; }
    }
}
