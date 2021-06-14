using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoAutofacSingletonCommandHandler : ICommandHandler<DemoAutofacSingletonCommand, CommonResponse<string>>
    {
        private readonly IAutofacISingletonService _service;
        public DemoAutofacSingletonCommandHandler(IAutofacISingletonService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<string>> Handle(IReceiveContext<DemoAutofacSingletonCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<string>() { Data = (await _service.GetGuid().ConfigureAwait(false)).ToString(), Code = 200, Message = "OK" };
        }
    }
}
