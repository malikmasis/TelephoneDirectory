using System;

namespace TelephoneDirectory.Contracts.Eto;

public sealed record OrderSubmissionRejected
{
    public Guid CorrelationId { get; }
    public Guid OrderId { get; }
    public string Reason { get; }
    public string ReportId { get; }
    public string FaultMessage { get; }
    public DateTime FaultTime { get; }
}
