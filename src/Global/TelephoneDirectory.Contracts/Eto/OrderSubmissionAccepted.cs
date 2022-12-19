using System;

namespace TelephoneDirectory.Contracts.Eto
{
    public record OrderSubmissionAccepted
    {
        public string ReportId { get; }
        public Guid CorrelationId { get; }
    }
}
