using System.Threading;
using System.Threading.Tasks;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo.FluentValidation;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo.FluentValidation;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo.FluentValidation
{
    public class DemoFluentValidationUpdateCommandHandler : ICommandHandler<DemoFluentValidationUpdateCommand, CommonResponse<bool>>
    {
        private readonly IFluentValidation _service;

        public DemoFluentValidationUpdateCommandHandler(IFluentValidation service)
        {
            _service = service;
        }
        public async Task<CommonResponse<bool>> Handle(IReceiveContext<DemoFluentValidationUpdateCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<bool>() { Code = 200, Data = await _service.UpdateTest(context.Message, cancellationToken).ConfigureAwait(false), Message = "OK" };
        }
    }
}
