using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo.Autofac;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoAutofacInstancePerLifetimeCommandHandler : ICommandHandler<DemoAutofacInstancePerLifetimeCommand, CommonResponse<string>>
    {
        private readonly IAutofacInstancePerLifetimeService _service;
        public DemoAutofacInstancePerLifetimeCommandHandler(IAutofacInstancePerLifetimeService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<string>> Handle(IReceiveContext<DemoAutofacInstancePerLifetimeCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<string>() { Data = (await _service.GetGuid().ConfigureAwait(false)).ToString(), Code = 200, Message = "OK" };
        }
    }
}
