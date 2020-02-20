using System;
using Autofac;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Services;

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
                .As<IUserServices>();

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
