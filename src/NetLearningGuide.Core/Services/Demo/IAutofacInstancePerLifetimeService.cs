using NetLearningGuide.Core.Services.ServiceLifetime;
using System;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
{
    public interface IAutofacInstancePerLifetimeService : IInstancePerLifetimeService
    {
        Task<Guid> GetGuid();
    }
}
