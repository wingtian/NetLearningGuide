using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
{
    public interface IDemoAutoMapperService : ISingletonService
    {
        Task<DemoMappingDto> AutoMapperTest(DemoMappingServiceCommand demoMappingCommand, CancellationToken cancellationToken);
    }
}
