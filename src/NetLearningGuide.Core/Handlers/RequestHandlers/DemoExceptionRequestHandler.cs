using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Dtos.Demo;
using NetLearningGuide.Message.Requests.Demo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.RequestHandlers
{
    public class DemoExceptionRequestHandler : IRequestHandler<DemoExceptionRequest, CommonResponse<DemoDto>>
    {
        public async Task<CommonResponse<DemoDto>> Handle(IReceiveContext<DemoExceptionRequest> context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                throw new Exception("System Exception");
            }).ConfigureAwait(false);
            return await Task.Run(() =>
            {
                return new CommonResponse<DemoDto>() { Code = 200, Data = new DemoDto(new Guid()), Message = "OK" };
            }).ConfigureAwait(false);
        }
    }
}
