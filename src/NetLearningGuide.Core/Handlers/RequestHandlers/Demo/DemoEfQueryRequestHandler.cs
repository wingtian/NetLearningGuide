using System.Threading;
using System.Threading.Tasks;
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.Services.Demo;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Requests.Demo;

namespace NetLearningGuide.Core.Handlers.RequestHandlers.Demo
{
    public class DemoEfQueryRequestHandler : IRequestHandler<DemoEfQueryRequest, CommonResponse<TestDbUp>>
    {
        private readonly IEfCoreTestService _service;

        public DemoEfQueryRequestHandler(IEfCoreTestService service)
        {
            _service = service;
        }
        public async Task<CommonResponse<TestDbUp>> Handle(IReceiveContext<DemoEfQueryRequest> context, CancellationToken cancellationToken)
        {
            return new CommonResponse<TestDbUp>() { Code = 200, Data = await _service.GetTest(context.Message.Id, cancellationToken).ConfigureAwait(false), Message = "OK" };
        }
    }
}
