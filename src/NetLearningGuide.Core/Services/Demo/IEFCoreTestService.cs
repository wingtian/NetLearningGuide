using NetLearningGuide.Core.Domain.Demo;
using NetLearningGuide.Core.Services.ServiceLifetime;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
{
    public interface IEFCoreTestService : IInstancePerDependencyService
    {
        Task<bool> InsertTest(Guid guid, CancellationToken cancellationToken);
        Task<bool> UpdateTest(Guid guid, CancellationToken cancellationToken);
        Task<TestDbUp> GetTest(Guid guid, CancellationToken cancellationToken);
    }
}
