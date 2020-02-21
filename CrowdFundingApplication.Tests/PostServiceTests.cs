using Xunit;
using System;
using Autofac;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using CrowdFundingApplication.Core;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.User;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.MediaOptions;
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.ProjectOptions;

namespace CrowdFundingApplication.Tests
{
    public class PostServiceTests : IClassFixture<CrowdFundingApplicationFixture>
    {

        private readonly IPostService post_;
        private readonly IProjectService pjct_;
        private readonly IUserService usr_;
        private readonly CrowdFundingDbContext context_;


        public PostServiceTests(CrowdFundingApplicationFixture fixture)
        {
            context_ = fixture.DbContext;
            pjct_ = fixture.Container.Resolve<IProjectService>();
            post_ = fixture.Container.Resolve<IPostService>();
            usr_ = fixture.Container.Resolve<IUserService>();
        }

        [Fact]
        public async Task AddPost_Success()
        {
            var project = 1;
            var user = 1;
        }

    }
}
