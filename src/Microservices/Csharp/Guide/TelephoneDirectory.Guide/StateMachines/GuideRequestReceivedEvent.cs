using System;
using TelephoneDirectory.Contracts.Abstraction;

namespace TelephoneDirectory.Guide.StateMachines
{
    public class GuideRequestReceivedEvent : IGuideRequestReceivedEvent
    {
        private readonly GuideSagaState _reportSagaState;
        public GuideRequestReceivedEvent(GuideSagaState reportSagaState)
        {
            _reportSagaState = reportSagaState;
        }

        public Guid CorrelationId => _reportSagaState.CorrelationId;

        public string ReportId => _reportSagaState.ReportId;
    }
}
