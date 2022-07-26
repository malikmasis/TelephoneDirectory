using System;
using Automatonymous;
using TelephoneDirectory.Contracts;

namespace TelephoneDirectory.Guide.StateMachines
{
    public class GuideStateMachine : MassTransitStateMachine<GuideSagaState>
    {
        public State Submitted { get; private set; }
        public State Processed { get; private set; }

        public Event<IGuideRequestCommand> CreateReportCommandReceived { get; private set; }
        public Event<IGuideRequestReceivedEvent> ReportRequestReceived { get; private set; }

        public Event<OrderSubmissionAccepted> ReportCreated { get; private set; }
        public Event<OrderSubmissionRejected> ReportFailed { get; private set; }


        public GuideStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreateReportCommandReceived, cc => cc
                        .CorrelateBy(state => state.ReportId, context => context.Message.ReportId)
                        .SelectId(context => Guid.NewGuid()));
            Event(() => ReportRequestReceived, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => ReportCreated, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => ReportFailed, x => x.CorrelateById(context => context.Message.CorrelationId));

            During(Initial, new Automatonymous.Binders.EventActivities<GuideSagaState>[] 
            { 
                When(CreateReportCommandReceived).Then(context =>
                {
                    context.Instance.ReportId = context.Data.ReportId;
                })
                .Publish(ctx => new GuideRequestReceivedEvent(ctx.Instance))
                .TransitionTo(Submitted)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
            });

            During(Submitted, new Automatonymous.Binders.EventActivities<GuideSagaState>[] 
            {
                When(ReportRequestReceived)
                .TransitionTo(Processed)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
            });

            During(Processed, new Automatonymous.Binders.EventActivities<GuideSagaState>[]
            {
                When(ReportCreated).Then(context =>
                {
                    context.Instance.ReportId = context.Data.ReportId;
                })
                .Publish(ctx => new GuideCreatedEvent(ctx.Instance)).Finalize()
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString())),

                When(ReportFailed).Then(context =>
                {
                    context.Instance.ReportId = context.Data.ReportId;

                    context.Instance.FaultMessage = context.Data.FaultMessage;
                    context.Instance.FaultTime = context.Data.FaultTime;
                })
                .Publish(ctx => new GuideFailedEvent(ctx.Instance)).Finalize()
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
            });
        }
    }
}
