using System.Threading.Tasks;
using NetLearningGuide.Core.Services.ServiceLifetime;

namespace NetLearningGuide.Core.Services.Demo.Configuration
{
    public interface IConfigurationService : IInstancePerLifetimeService
    {
        Task<string> GetAudienceConfiguration();
    }
}
