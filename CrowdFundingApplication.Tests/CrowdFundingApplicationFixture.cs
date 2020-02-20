using System;
using Autofac;
using CrowdFundingApplication.Core;
using CrowdFundingApplication.Core.Data;

namespace CrowdFundingApplication.Tests
{
    public class CrowdFundingApplicationFixture : IDisposable
    {
        public CrowdFundingDbContext DbContext { get; private set; }

        public ILifetimeScope Container { get; private set; }

        public CrowdFundingApplicationFixture()
        {
            Container = ServiceRegistrator
                .CreateContainer()
                .BeginLifetimeScope();

            DbContext = Container.Resolve<CrowdFundingDbContext>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
