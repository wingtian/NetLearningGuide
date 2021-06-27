using System.Threading;
using System.Threading.Tasks;
using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;

namespace NetLearningGuide.Core.Services.Demo.AutoMapper
{
    public interface IDemoAutoMapperService : ISingletonService
    {
        Task<DemoMappingDto> AutoMapperTest(DemoMappingServiceCommand demoMappingCommand, CancellationToken cancellationToken);
    }
}
