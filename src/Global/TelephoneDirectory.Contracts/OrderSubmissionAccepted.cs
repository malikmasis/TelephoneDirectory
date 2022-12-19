using System;

namespace TelephoneDirectory.Contracts
{
    public record OrderSubmissionAccepted
    {
        public string ReportId { get; }
        public Guid CorrelationId { get; }
    }
}
