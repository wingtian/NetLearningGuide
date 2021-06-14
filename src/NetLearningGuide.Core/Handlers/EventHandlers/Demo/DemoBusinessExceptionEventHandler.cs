using System.Threading;
using System.Threading.Tasks;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Exceptions;
using NetLearningGuide.Message.Events.Demo;

namespace NetLearningGuide.Core.Handlers.EventHandlers.Demo
{
    public class DemoBusinessExceptionEventHandler : IEventHandler<DemoBusinessExceptionEvent>
    {
        public async Task Handle(IReceiveContext<DemoBusinessExceptionEvent> context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                throw new BusinessException(500, "BusinessException");
            }).ConfigureAwait(false);
            await Task.CompletedTask;
        }
    }
}
