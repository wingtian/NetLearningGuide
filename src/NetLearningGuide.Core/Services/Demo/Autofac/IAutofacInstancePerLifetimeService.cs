using System;
using System.Threading.Tasks;
using NetLearningGuide.Core.Services.ServiceLifetime;

namespace NetLearningGuide.Core.Services.Demo.Autofac
{
    public interface IAutofacInstancePerLifetimeService : IInstancePerLifetimeService
    {
        Task<Guid> GetGuid();
    }
}
