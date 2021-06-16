using Microsoft.Extensions.Configuration;
using NetLearningGuide.Core.ConfigurationSetting;

namespace NetLearningGuide.Core.Settings
{
    public class MySqlConnectionString : ConfigurationSetting<string>
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public MySqlConnectionString(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override string Value
        {
            get
            {
                if (!string.IsNullOrEmpty(_connectionString))
                {
                    return _connectionString;
                }

                var connectionString = _configuration.GetConnectionString("Mysql");

                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = _configuration.GetValue<string>("Mysql");
                }

                return connectionString;
            }
        }
    }
}
