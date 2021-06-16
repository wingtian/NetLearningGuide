using Mediator.Net.Context;
using Mediator.Net.Contracts;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using NetLearningGuide.Message.Dtos.Demo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Handlers.CommandHandlers.Demo
{
    public class DemoEFInsertCommandHandler : ICommandHandler<DemoEFInsertCommand, CommonResponse<bool>>
    {
        public async Task<CommonResponse<bool>> Handle(IReceiveContext<DemoEFInsertCommand> context, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return new CommonResponse<bool>() { Code = 200, Data =true  , Message = "OK" };
            }).ConfigureAwait(false);
        }
    }
}
