using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoEfInsertCommandHandler : ICommandHandler<DemoEfInsertCommand, CommonResponse<bool>>
    {
        private readonly IEfCoreTestService _service;

        public DemoEfInsertCommandHandler(IEfCoreTestService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<bool>> Handle(IReceiveContext<DemoEfInsertCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<bool>() { Code = 200, Data = await _service.InsertTest(context.Message, cancellationToken).ConfigureAwait(false), Message = "OK" };
        }
    }
}
