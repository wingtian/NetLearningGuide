using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Events.Demo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.EventHandlers.Demo
{
    public class DemoExceptionEventHandler : IEventHandler<DemoExceptionEvent>
    {
        public async Task Handle(IReceiveContext<DemoExceptionEvent> context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                throw new Exception("System Exception");
            }).ConfigureAwait(false);
            await Task.CompletedTask;
        }
    }
}
