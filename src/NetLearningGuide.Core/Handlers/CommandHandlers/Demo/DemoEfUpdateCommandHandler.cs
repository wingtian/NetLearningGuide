using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo.EfCore;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoEfUpdateCommandHandler : ICommandHandler<DemoEfUpdateCommand, CommonResponse<bool>>
    {
        private readonly IEfCoreTestService _service;

        public DemoEfUpdateCommandHandler(IEfCoreTestService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<bool>> Handle(IReceiveContext<DemoEfUpdateCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<bool>() { Code = 200, Data = await _service.UpdateTest(context.Message.Id, cancellationToken).ConfigureAwait(false), Message = "OK" };
        }
    }
}
