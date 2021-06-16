using DbUp;
using System.Reflection;

namespace NetLearningGuide.Core.DbUp.Tool
{
    public class DbUpSetUpMysql
    {
        public DbUpSetUpMysql(string mySqlConnection)
        {
            EnsureDatabase.For.MySqlDatabase(mySqlConnection);
            DeployChanges.To
                .MySqlDatabase(mySqlConnection)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build().PerformUpgrade();
        }
    }
}
