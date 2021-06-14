using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using Mediator.Net;
using Mediator.Net.Autofac;
using NetLearningGuide.Core.Middlewares;
using NetLearningGuide.Core.Services;
using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Mappings;
using Module = Autofac.Module;

namespace NetLearningGuide.Core.Settings
{
    public class NetLearningGuideModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMediator(builder);
            RegisterServices(builder);
            RegisterAutoMapper(builder);
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
                    case bool _ when typeof(IInstancePerDependencyService).IsAssignableFrom(type):
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
                        break; 
                    default:
                        builder.RegisterType(type).AsSelf().AsImplementedInterfaces();
                        break;
                }
            }
        }
        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(NetProfile).GetTypeInfo().Assembly.GetTypes());
                }))
                .AsSelf()
                .SingleInstance();

            builder.Register(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
