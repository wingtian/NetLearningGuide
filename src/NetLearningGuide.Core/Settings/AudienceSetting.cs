using Microsoft.Extensions.Configuration;
using NetLearningGuide.Core.ConfigurationSetting;

namespace NetLearningGuide.Core.Settings
{
    public class AudienceSetting : ConfigurationSetting<string>
    {
        private readonly IConfiguration _configuration;

        public AudienceSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Value => _configuration.GetValue<string>("JWT:Audience");
    }
}
