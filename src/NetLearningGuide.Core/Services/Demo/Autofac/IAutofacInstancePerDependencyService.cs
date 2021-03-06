using System;
using System.Threading.Tasks;
using NetLearningGuide.Core.Services.ServiceLifetime;

namespace NetLearningGuide.Core.Services.Demo.Autofac
{
    public interface IAutofacInstancePerDependencyService : IInstancePerDependencyService
    {
        Task<Guid> GetGuid();
        Task InitalClass();
    }
}
