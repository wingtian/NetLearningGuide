using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using Mediator.Net;
using Mediator.Net.Autofac;
using Microsoft.EntityFrameworkCore;
using NetLearningGuide.Core.ConfigurationSetting;
using NetLearningGuide.Core.DbUp.Tool;
using NetLearningGuide.Core.EFCore;
using NetLearningGuide.Core.Middlewares;
using NetLearningGuide.Core.Services;
using NetLearningGuide.Core.Services.ServiceLifetime;
using NetLearningGuide.Core.Settings;
using NetLearningGuide.Core.Validators;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Mappings;

namespace NetLearningGuide.Core.Module
{
    public class NetLearningGuideModule : Autofac.Module
    {
        private readonly DbUpSetting _dbUpSetting;

        public NetLearningGuideModule(DbUpSetting dbUpSetting)
        {
            _dbUpSetting = dbUpSetting;
        }
        protected override void Load(ContainerBuilder builder)
        {
            RegisterSettings(builder);
            RegisterMediator(builder);
            RegisterServices(builder);
            RegisterAutoMapper(builder);
            RegisterDatabase(builder);
            RegisterDbUp(builder);
            RegisterValidator(builder);
        }
        private void RegisterDbUp(ContainerBuilder builder)
        {
            if (_dbUpSetting == null)
                return;
            if (_dbUpSetting.ShouldRunDbUp)
            {
                builder.RegisterInstance(new DbUpSetUpMysql(_dbUpSetting.DbUpConnectionString));
            }
        }
        private void RegisterSettings(ContainerBuilder builder)
        {
            builder.RegisterTypes(typeof(NetLearningGuideModule).Assembly.GetTypes()
                    .Where(x => x.IsClass && typeof(IConfigurationSetting).IsAssignableFrom(x)).ToArray()).AsSelf()
                .SingleInstance();
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
        public class DbUpSetting
        {
            public bool ShouldRunDbUp { get; set; }
            public string DbUpConnectionString { get; set; }
        }
        private void RegisterDatabase(ContainerBuilder builder)
        {
            if (_dbUpSetting == null)
                return;
            builder.RegisterInstance(new MySqlConnectionString(_dbUpSetting.DbUpConnectionString));
            builder.RegisterType<DbNetContext>()
                .AsSelf().As<DbContext>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
        private void RegisterValidator(ContainerBuilder builder)
        {
            builder.RegisterTypes(typeof(NetLearningGuideModule).Assembly.GetTypes()
                    .Where(x => x.IsClass && typeof(IEntityFluentValidator).IsAssignableFrom(x)).ToArray()).AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterTypes(typeof(NetLearningGuideModule).Assembly.GetTypes()
                    .Where(x => x.IsClass && typeof(IEntityBusinessLogicValidator).IsAssignableFrom(x)).ToArray())
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
