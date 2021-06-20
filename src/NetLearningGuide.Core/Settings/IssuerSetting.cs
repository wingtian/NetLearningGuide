using Microsoft.Extensions.Configuration;
using NetLearningGuide.Core.ConfigurationSetting;

namespace NetLearningGuide.Core.Settings
{
    public class IssuerSetting : ConfigurationSetting<string>
    {
        private readonly IConfiguration _configuration;

        public IssuerSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override string Value => _configuration.GetValue<string>("JWT:Issuer");
    }
}
