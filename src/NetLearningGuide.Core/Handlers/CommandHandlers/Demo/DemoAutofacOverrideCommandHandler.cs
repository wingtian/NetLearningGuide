using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo.Autofac;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoAutofacOverrideCommandHandler : ICommandHandler<DemoAutofacOverrideCommand, CommonResponse<string>>
    {
        private readonly IAutofacInstancePerDependencyService _service;
        public DemoAutofacOverrideCommandHandler(IAutofacInstancePerDependencyService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<string>> Handle(IReceiveContext<DemoAutofacOverrideCommand> context, CancellationToken cancellationToken)
        {
            await _service.InitalClass().ConfigureAwait(false);
            return new CommonResponse<string>() { Data = (await _service.GetGuid().ConfigureAwait(false)).ToString(), Code = 200, Message = "OK" };
        }
    }
}
