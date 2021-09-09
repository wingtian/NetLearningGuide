using System;
using System.Threading.Tasks;
using Autofac;
using NetLearningGuide.Message.Dtos.Demo;

namespace NetLearningGuide.Core.Services.Demo.Autofac
{
    public class AutofacInstancePerDependencyService : IAutofacInstancePerDependencyService
    {
        private Guid id;
        private AutoFacTestClass temp;
        private ContainerBuilder containerBuilder;
        public AutofacInstancePerDependencyService(AutoFacTestClass temp, ContainerBuilder _containerBuilder)
        {
            this.temp = temp;
            id = Guid.NewGuid();
            containerBuilder = _containerBuilder;
        }
        public async Task<Guid> GetGuid()
        {
            return await Task.Run(() =>
            {
                return id;
            }).ConfigureAwait(false);
        }

        public async Task InitalClass()
        { 
            var test = temp.Test;
            var model = new AutoFacTestClass("Service");
            containerBuilder.RegisterInstance(model).SingleInstance();
        }
    }
}
