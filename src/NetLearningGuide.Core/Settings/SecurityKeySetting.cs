using Microsoft.Extensions.Configuration;
using NetLearningGuide.Core.ConfigurationSetting;

namespace NetLearningGuide.Core.Settings
{
    public class SecurityKeySetting : ConfigurationSetting<string>
    {
        private readonly IConfiguration _configuration;

        public SecurityKeySetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Value => _configuration.GetValue<string>("JWT:SecurityKey");
    }
}
