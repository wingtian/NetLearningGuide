using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo.Autofac;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoAutofacInstancePerDependencyCommandHandler : ICommandHandler<DemoAutofacInstancePerDependencyCommand, CommonResponse<string>>
    {
        private readonly IAutofacInstancePerDependencyService _service;
        public DemoAutofacInstancePerDependencyCommandHandler(IAutofacInstancePerDependencyService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<string>> Handle(IReceiveContext<DemoAutofacInstancePerDependencyCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<string>() { Data = (await _service.GetGuid().ConfigureAwait(false)).ToString(), Code = 200, Message = "OK" };
        }
    }
}
