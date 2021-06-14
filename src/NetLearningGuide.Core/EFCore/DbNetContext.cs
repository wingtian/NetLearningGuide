using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.MySqlDomain;
using NetLearningGuide.Core.Settings;
using NetLearningGuide.Util;
using System.Linq;
using System.Reflection;

namespace NetLearningGuide.Core.EFCore
{
    public class DbNetContext : DbContext
    {
        private readonly MySqlConnectionString _connectionString;
        public DbNetContext(MySqlConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySQL(_connectionString.Value, builder => builder.CommandTimeout(6000));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            typeof(DbNetContext).GetTypeInfo().Assembly.GetTypes()
                .Where(t => typeof(IEntity).IsAssignableFrom(t) && t.IsClass)
                .ForEach(x =>
                {
                    if (modelBuilder.Model.FindEntityType(x) == null)
                        modelBuilder.Model.AddEntityType(x);
                });
        }
    }
}
