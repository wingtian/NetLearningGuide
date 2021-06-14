using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoAutoMapperServiceCommandHandler : ICommandHandler<DemoMappingServiceCommand, CommonResponse<DemoMappingDto>>
    {
        private readonly IDemoAutoMapperService _service;
        public DemoAutoMapperServiceCommandHandler(IDemoAutoMapperService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<DemoMappingDto>> Handle(IReceiveContext<DemoMappingServiceCommand> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<DemoMappingDto>() { Code = 200, Data = await _service.AutoMapperTest(context.Message, cancellationToken).ConfigureAwait(false), Message = "OK" };
        }
    } 
}
