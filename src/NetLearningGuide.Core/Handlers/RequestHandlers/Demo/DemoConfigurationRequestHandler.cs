using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Services.Demo;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Requests.Demo;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.RequestHandlers.Demo
{
    public class DemoConfigurationRequestHandler : IRequestHandler<DemoConfigurationRequest, CommonResponse<string>>
    {
        private readonly IConfigurationService _service;
        public DemoConfigurationRequestHandler(IConfigurationService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<string>> Handle(IReceiveContext<DemoConfigurationRequest> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<string>() { Data = await _service.GetAudienceConfiguration().ConfigureAwait(false) };
        }
    }
}
