using Autofac;
using NetLearningGuide.Core.Settings;
using System;
using System.Threading.Tasks;

namespace NetLearningGuide.IntegrationTests
{
    public class TestBase : IDisposable
    {
        private ILifetimeScope _container;
        private ContainerBuilder Builder { get; set; }
        private string mysqlConnection = "server=localhost;uid=root;pwd=123456;database=db_net_leaning;";
        private static object _baseLock = new object();
        private static bool _isRunDbUp = true;

        protected TestBase()
        {
            lock (_baseLock)
            {
                Builder = new ContainerBuilder();
                Builder.RegisterModule(new NetLearningGuideModule(new NetLearningGuideModule.DbUpSetting()
                {
                    DbUpConnectionString = mysqlConnection,
                    ShouldRunDbUp = _isRunDbUp,
                }));
                _isRunDbUp = false;
                _container = Builder.Build();
            }
        }
        protected async Task Run<T>(Func<T, Task> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var dependency = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration).Resolve<T>()
                : _container.BeginLifetimeScope().Resolve<T>();
            await action(dependency);
        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
