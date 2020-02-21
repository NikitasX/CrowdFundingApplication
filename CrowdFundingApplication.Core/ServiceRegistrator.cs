using System;
using Autofac;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Services;
using CrowdFundingApplication.Core.Services.Interfaces;

namespace CrowdFundingApplication.Core
{
    public class ServiceRegistrator : Module
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterServices(builder);

            return builder.Build();
        }

        public static void RegisterServices(ContainerBuilder builder)
        {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

            builder
                .RegisterType<UserService>()
                .InstancePerLifetimeScope()
                .As<IUserService>();            
            
            builder
                .RegisterType<ProjectService>()
                .InstancePerLifetimeScope()
                .As<IProjectService>();              
            
            builder
                .RegisterType<MediaService>()
                .InstancePerLifetimeScope()
                .As<IMediaService>();              
            
            builder
                .RegisterType<PostService>()
                .InstancePerLifetimeScope()
                .As<IPostService>();             
            
            builder
                .RegisterType<IncentiveService>()
                .InstancePerLifetimeScope()
                .As<IIncentiveService>();            
            
            builder
                .RegisterType<LoggerService>()
                .InstancePerLifetimeScope()
                .As<ILoggerService>();

            builder
                .RegisterType<CrowdFundingDbContext>()
                .InstancePerLifetimeScope()
                .AsSelf();
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
        }
    }
}
