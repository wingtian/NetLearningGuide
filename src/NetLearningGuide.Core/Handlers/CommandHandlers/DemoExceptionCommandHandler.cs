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
    public class DemoBusinessExceptionCommandHandler : ICommandHandler<DemoExceptionCommand, CommonResponse<DemoDto>>
    {
        public async Task<CommonResponse<DemoDto>> Handle(IReceiveContext<DemoExceptionCommand> context, CancellationToken cancellationToken)
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
