namespace NetLearningGuide.Core.Settings
{
    public class MySqlConnectionString
    {
        private readonly string _connectionString;
        public MySqlConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string Value => _connectionString;
    }
}
