using System;
using System.Threading.Tasks;
using Autofac;
using NetLearningGuide.Core.Settings;

namespace NetLearningGuide.UnitTest.Demo
{
    public class DemoBase : IDisposable
    {
        private readonly ILifetimeScope _container;
        private ContainerBuilder Builder { get; set; }
        protected DemoBase()
        {
            Builder = new ContainerBuilder();
            Builder.RegisterModule(new NetLearningGuideModule());
            _container = Builder.Build();
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
