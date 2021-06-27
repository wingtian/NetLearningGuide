using System;
using System.Threading;
using System.Threading.Tasks;
using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Message.Commands.Demo;

namespace NetLearningGuide.Core.Services.Demo.EfCore
{
    public interface IEfCoreTestService : IInstancePerDependencyService
    {
        Task<bool> InsertTest(DemoEfInsertCommand model, CancellationToken cancellationToken);
        Task<bool> UpdateTest(Guid guid, CancellationToken cancellationToken);
        Task<TestDbUp> GetTest(Guid guid, CancellationToken cancellationToken);
        Task<bool> DeleteTest(Guid guid, CancellationToken cancellationToken);
    }
}
