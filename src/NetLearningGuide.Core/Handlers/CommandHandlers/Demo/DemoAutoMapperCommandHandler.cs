using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoAutoMapperCommandHandler : ICommandHandler<DemoMappingCommand, CommonResponse<DemoMappingDto>>
    {
        private readonly IMapper _mapper;
        public DemoAutoMapperCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<CommonResponse<DemoMappingDto>> Handle(IReceiveContext<DemoMappingCommand> context, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return new CommonResponse<DemoMappingDto>() { Code = 200, Data = _mapper.Map<DemoMappingDto>(context.Message), Message = "OK" };
            }).ConfigureAwait(false);
        }
    } 
}
