using System;

namespace TelephoneDirectory.Contracts.Eto;

public sealed record OrderSubmissionAccepted
{
    public string ReportId { get; }
    public Guid CorrelationId { get; }
}
