using System.Threading.Tasks;
using NetLearningGuide.Core.Settings;

namespace NetLearningGuide.Core.Services.Demo.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AudienceSetting _setting;
        public ConfigurationService(AudienceSetting setting)
        {
            _setting = setting;
        }
        public async Task<string> GetAudienceConfiguration()
        {
            return await Task.Run(() =>
            {
                return _setting;
            }).ConfigureAwait(false);
        }
    }
}
