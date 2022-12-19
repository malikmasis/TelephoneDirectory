using MassTransit;
using System;
using TelephoneDirectory.Contracts.Abstraction;
using TelephoneDirectory.Contracts.Eto;

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

            During(Initial, new EventActivities<GuideSagaState>[]
            {
                When(CreateReportCommandReceived).Then(context =>
                {
                    context.Saga.ReportId= context.MessageId.Value.ToString();
                })
                .Publish(ctx => new GuideRequestReceivedEvent(ctx.Saga))
                .TransitionTo(Submitted)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Saga.ToString()))
            });

            During(Submitted, new EventActivities<GuideSagaState>[]
            {
                When(ReportRequestReceived)
                .TransitionTo(Processed)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Saga.ToString()))
            });

            During(Processed, new EventActivities<GuideSagaState>[]
            {
                When(ReportCreated).Then(context =>
                {
                    context.Saga.ReportId = context.MessageId.Value.ToString();
                })
                .Publish(ctx => new GuideCreatedEvent(ctx.Saga)).Finalize()
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Saga.ToString())),

                When(ReportFailed).Then(context =>
                {
                    context.Saga.ReportId = context.MessageId.Value.ToString();
                    context.Saga.FaultMessage = context.Message.FaultMessage;
                    context.Saga.FaultTime = context.Message.FaultTime;
                })
                .Publish(ctx => new GuideFailedEvent(ctx.Saga)).Finalize()
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Saga.ToString()))
            });
        }
    }
}
