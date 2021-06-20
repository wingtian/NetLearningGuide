using Autofac;
using Microsoft.Extensions.Configuration;
using NetLearningGuide.Core.Module;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetLearningGuide.IntegrationTests
{
    public class TestBase : IDisposable
    {
        protected IConfiguration Configuration;
        private ILifetimeScope _container;
        private ContainerBuilder Builder { get; set; } 
        private static object _baseLock = new object();
        private static bool _isRunDbUp = true;

        protected TestBase()
        {
            lock (_baseLock)
            {
                Builder = new ContainerBuilder();
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                Builder.RegisterInstance(Configuration).As<IConfiguration>();
                Builder.RegisterModule(new NetLearningGuideModule(new NetLearningGuideModule.DbUpSetting()
                {
                    DbUpConnectionString = Configuration.GetConnectionString("Mysql"),
                    ShouldRunDbUp = _isRunDbUp,
                }));
                _isRunDbUp = false;
                _container = Builder.Build();
            }
        }
        protected void Run<T>(Action<T> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var dependency = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration).Resolve<T>()
                : _container.BeginLifetimeScope().Resolve<T>();
            action(dependency);
        }

        protected void Run<T, TR>(Action<T, TR> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var lifetime = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration)
                : _container.BeginLifetimeScope();

            var dependency = lifetime.Resolve<T>();
            var dependency2 = lifetime.Resolve<TR>();
            action(dependency, dependency2);
        }
        protected async Task Run<T, TR, TL>(Func<T, TR, TL, Task> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var lifetime = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration)
                : _container.BeginLifetimeScope();

            var dependency = lifetime.Resolve<T>();
            var dependency2 = lifetime.Resolve<TR>();
            var dependency3 = lifetime.Resolve<TL>();
            await action(dependency, dependency2, dependency3);
        }

        protected async Task Run<T>(Func<T, Task> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var dependency = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration).Resolve<T>()
                : _container.BeginLifetimeScope().Resolve<T>();
            await action(dependency);
        }

        protected async Task<TR> Run<T, TR>(Func<T, Task<TR>> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var dependency = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration).Resolve<T>()
                : _container.BeginLifetimeScope().Resolve<T>();
            return await action(dependency);
        }

        protected TR Run<T, TR>(Func<T, TR> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var dependency = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration).Resolve<T>()
                : _container.BeginLifetimeScope().Resolve<T>();
            return action(dependency);
        }

        protected TR Run<T, TU, TR>(Func<T, TU, TR> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var lifetime = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration)
                : _container.BeginLifetimeScope();

            var dependency = lifetime.Resolve<T>();
            var dependency2 = lifetime.Resolve<TU>();
            return action(dependency, dependency2);
        }

        protected Task Run<T, TU>(Func<T, TU, Task> action, Action<ContainerBuilder> extraRegistration = null)
        {
            var lifetime = extraRegistration != null
                ? _container.BeginLifetimeScope(extraRegistration)
                : _container.BeginLifetimeScope();
            var dependency = lifetime.Resolve<T>();
            var dependency2 = lifetime.Resolve<TU>();
            return action(dependency, dependency2);
        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
