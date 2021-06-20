using NetLearningGuide.Core.Settings;
using System.Threading.Tasks;

namespace NetLearningGuide.Core.Services.Demo
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
