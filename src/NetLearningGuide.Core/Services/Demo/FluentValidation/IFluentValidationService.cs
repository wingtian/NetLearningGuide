using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Message.Commands.Demo.FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo.FluentValidation
{
    public interface IFluentValidation : IInstancePerDependencyService
    {
        Task<bool> InsertTest(DemoFluentValidationInsertCommand command, CancellationToken cancellationToken);
        Task<bool> UpdateTest(DemoFluentValidationUpdateCommand command, CancellationToken cancellationToken); 
    }
}
