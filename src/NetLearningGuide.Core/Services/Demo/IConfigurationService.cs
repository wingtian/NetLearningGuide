using NetLearningGuide.Core.Services.ServiceLifetime;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
{
    public interface IConfigurationService : IInstancePerLifetimeService
    {
        Task<string> GetAudienceConfiguration();
    }
}
