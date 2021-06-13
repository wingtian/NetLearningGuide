using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Events.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.EventHandlers
{
    public class DemoEventHandler : IEventHandler<DemoEvent>
    {
        public async Task Handle(IReceiveContext<DemoEvent> context, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
