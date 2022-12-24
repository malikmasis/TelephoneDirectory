using MassTransit;
using System;

namespace TelephoneDirectory.Guide.StateMachines;

public sealed class GuideSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public State CurrentState { get; set; }
    public string ReportId { get; set; }
    public string FaultMessage { get; set; }
    public DateTime FaultTime { get; set; }
}
