using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class DbUpTest : GeneralTestFixtureBase
    {
        [Fact]
        public async Task TestDbUp()
        {
            await using var connection = new MySqlConnection(Configuration.GetConnectionString("Mysql"));
            var command = "show databases like 'db_net_leaning';";
            connection.Open();
            await using var cmd = new MySqlCommand(command, connection);
            var data = cmd.ExecuteReader();
            data.Read().ShouldBeTrue();
            data["Database (db_net_leaning)"].ToString().ShouldBe("db_net_leaning");
            connection.Close();
            connection.Open();
            command = "SELECT table_name FROM information_schema.TABLES WHERE table_name ='test_dbup'";
            await using var cmdSchema = new MySqlCommand(command, connection);
            var dataSchema = cmdSchema.ExecuteReader();
            dataSchema.Read().ShouldBeTrue();
            dataSchema["table_name"].ToString().ShouldBe("test_dbup");
            connection.Close();
        }
    }
}
