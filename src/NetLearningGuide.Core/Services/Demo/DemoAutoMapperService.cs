using AutoMapper;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
{
    public class DemoAutoMapperService : IDemoAutoMapperService
    {
        private readonly IMapper _mapper;
        public DemoAutoMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<DemoMappingDto> AutoMapperTest(DemoMappingServiceCommand demoMappingCommand, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
             {
                 return _mapper.Map<DemoMappingDto>(demoMappingCommand);
             }).ConfigureAwait(false); 
        }
    }
}
