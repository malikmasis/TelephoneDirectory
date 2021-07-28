using System;

namespace TelephoneDirectory.Contracts
{
    public interface OrderSubmissionAccepted
    {
        string ReportId { get; }
        Guid CorrelationId { get; }
    }
}
