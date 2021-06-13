using System.Linq;
using System.Reflection;
using Autofac;
using Mediator.Net;
using Mediator.Net.Autofac;
using NetLearningGuide.Core.Middlewares;
using NetLearningGuide.Core.Services;
using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Message.Basic;
using Module = Autofac.Module;

namespace NetLearningGuide.Core.Settings
{
    public class NetLearningGuideModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMediator(builder);
            RegisterServices(builder);
        }

        private void RegisterMediator(ContainerBuilder builder)
        {
            builder.RegisterMediator(MakeMediatorBuilder());
        }
        private static MediatorBuilder MakeMediatorBuilder()
        {
            var mediaBuilder = new MediatorBuilder();
            mediaBuilder.ConfigureGlobalReceivePipe(config => config.UnifyResponseMiddleware(typeof(CommonResponse<>)))
                .RegisterHandlers(typeof(NetLearningGuideModule).Assembly);
            return mediaBuilder;
        }
        private void RegisterServices(ContainerBuilder builder)
        {
            foreach (var type in typeof(IService).GetTypeInfo().Assembly.GetTypes()
                .Where(t => typeof(IService).IsAssignableFrom(t) && t.IsClass))
            {
                switch (true)
                {
                    case bool _ when typeof(IInstancePerLifetimeService).IsAssignableFrom(type):
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
                        break;
                    case bool _ when typeof(ISingletonService).IsAssignableFrom(type):
                        builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                        break;
                    case bool _ when typeof(ISwitchWithInstancePerLifetimeService).IsAssignableFrom(type):
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
                        break;
                    case bool _ when typeof(ISwitchWithSingletonService).IsAssignableFrom(type):
                        builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                        break;
                    default:
                        builder.RegisterType(type).AsSelf().AsImplementedInterfaces();
                        break;
                }
            }
        }
    }
}
