using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Dtos.Demo;
using NetLearningGuide.Message.Requests.Demo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.RequestHandlers.Demo
{
    public class DemoRequestHandler : IRequestHandler<DemoRequest, CommonResponse<DemoDto>>
    {
        public async Task<CommonResponse<DemoDto>> Handle(IReceiveContext<DemoRequest> context, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
              {
                  return new CommonResponse<DemoDto>() { Code = 200, Data = new DemoDto(new Guid()), Message = "OK" };
              }).ConfigureAwait(false);
        }
    }
}
