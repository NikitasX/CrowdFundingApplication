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
    public class MediaServiceTests : IClassFixture<CrowdFundingApplicationFixture>
    {

        private readonly IMediaService med_;
        private readonly IProjectService pjct_;
        private readonly CrowdFundingDbContext context_;


        public MediaServiceTests(CrowdFundingApplicationFixture fixture)
        {
            context_ = fixture.DbContext;
            pjct_ = fixture.Container.Resolve<IProjectService>();
            med_ = fixture.Container.Resolve<IMediaService>();
        }
      
        /// <summary>
        /// Add media tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddMedia_Success()
        {
            var media = await med_.AddMedia(1, new AddMediaOptions()
            {
                MediaType = MediaTypes.Image,
                MediaURL = "https://localhost:5001/"
            });

            Assert.NotNull(media.Data);
            Assert.Equal(StatusCode.Ok, media.ErrorCode);
        }

        [Fact]
        public async Task AddMedia_Failure_Null_Options()
        {
            var media = await med_.AddMedia(1, null);

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.BadRequest, media.ErrorCode);
        }

        [Fact]
        public async Task AddMedia_Failure_Wrong_Project_Id()
        {
            var media = await med_.AddMedia(0, new AddMediaOptions()
            {
                MediaType = MediaTypes.Image,
                MediaURL = "https://localhost:5001/"
            });

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.BadRequest, media.ErrorCode);
            Assert.Equal("Project id cannot be equal to or less than 0", media.ErrorText);
        }        
        
        [Fact]
        public async Task AddMedia_Failure_No_Media_URL()
        {
            var media = await med_.AddMedia(1, new AddMediaOptions()
            {
                MediaType = MediaTypes.Image,
                MediaURL = "    "
            });

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.BadRequest, media.ErrorCode);
            Assert.Equal("Media URL cannot be null or whitespace", media.ErrorText);
        }        
        
        [Fact]
        public async Task AddMedia_Failure_No_Media_Type()
        {
            var media = await med_.AddMedia(1, new AddMediaOptions()
            {
                MediaURL = "https://localhost:5001/"
            });

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.BadRequest, media.ErrorCode);
            Assert.Equal("Media Type not set (Invalid)", media.ErrorText);
        }        
        
        /// <summary>
        /// Remove media tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RemoveMedia_Success()
        {
            var media = await med_.RemoveMedia(3);

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.Ok, media.ErrorCode);
        }        
        
        [Fact]
        public async Task RemoveMedia_Failure_Wrong_Id()
        {
            var media = await med_.RemoveMedia(0);

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.BadRequest, media.ErrorCode);
        }        
        
        [Fact]
        public async Task RemoveMedia_Failure_Id_Not_Found()
        {
            var media = await med_.RemoveMedia(999999);

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.NotFound, media.ErrorCode);
        }        
        
        /// <summary>
        /// Get media by project id tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetMediaByProjectId_Success()
        {
            var media = await med_.GetMediaByProjectId(1);

            Assert.NotNull(media.Data);
            Assert.Equal(StatusCode.Ok, media.ErrorCode);
        }        
        
        [Fact]
        public async Task GetMediaByProjectId_Failure_Wrong_Id()
        {
            var media = await med_.GetMediaByProjectId(0);

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.BadRequest, media.ErrorCode);
        }        
        
        [Fact]
        public async Task GetMediaByProjectId_Failure_Id_Not_found()
        {
            var media = await med_.GetMediaByProjectId(999999);

            Assert.Null(media.Data);
            Assert.Equal(StatusCode.NotFound, media.ErrorCode);
        }
    }
}
