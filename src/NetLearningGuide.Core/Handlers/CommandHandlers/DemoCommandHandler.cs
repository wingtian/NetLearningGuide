using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers
{
    public class DemoCommandHandler : ICommandHandler<DemoCommand, CommonResponse<DemoDto>>
    {
        public async Task<CommonResponse<DemoDto>> Handle(IReceiveContext<DemoCommand> context, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return new CommonResponse<DemoDto>() { Code = 200, Data = new DemoDto(new Guid()), Message = "OK" };
            }).ConfigureAwait(false);
        }
    }
}
