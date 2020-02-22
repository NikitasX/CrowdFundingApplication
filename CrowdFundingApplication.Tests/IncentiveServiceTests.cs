using Xunit;
using Autofac;
using System.Threading.Tasks;
using CrowdFundingApplication.Core;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.IncentiveOptions;

namespace CrowdFundingApplication.Tests
{
    public class IncentiveServiceTests : IClassFixture<CrowdFundingApplicationFixture>
    {

        private readonly IIncentiveService inc_;
        private readonly IProjectService pjct_;
        private readonly IUserService usr_;
        private readonly CrowdFundingDbContext context_;


        public IncentiveServiceTests(CrowdFundingApplicationFixture fixture)
        {
            context_ = fixture.DbContext;
            pjct_ = fixture.Container.Resolve<IProjectService>();
            usr_ = fixture.Container.Resolve<IUserService>();
            inc_ = fixture.Container.Resolve<IIncentiveService>();
        }

        /// <summary>
        /// Add Incentive tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddIncentive_Success()
        {
            var project = 1;

            var incentive = await inc_.AddIncentive(project, new AddIncentiveOptions() { 
                IncentiveTitle = "Buy this for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 10,
                IncentiveReward = "Band t-shirt"
            });

            Assert.NotNull(incentive.Data);
            Assert.Equal(StatusCode.Ok, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task AddIncentive_Failure_Null_Options()
        {
            var project = 1;

            var incentive = await inc_.AddIncentive(project,null);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
        }

        [Fact]
        public async Task AddIncentive_Failure_Wrong_Project()
        {
            var project = 0;

            var incentive = await inc_.AddIncentive(project, new AddIncentiveOptions()
            {
                IncentiveTitle = "Buy this for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 10,
                IncentiveReward = "Band t-shirt"
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Project id cannot be equal to or less than 0", incentive.ErrorText);
        }
        
        [Fact]
        public async Task AddIncentive_Failure_No_Title()
        {
            var project = 1;

            var incentive = await inc_.AddIncentive(project, new AddIncentiveOptions()
            {
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 10,
                IncentiveReward = "Band t-shirt"
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Incentive title cannot be null or whitespace", incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentive_Failure_No_Price()
        {
            var project = 1;

            var incentive = await inc_.AddIncentive(project, new AddIncentiveOptions()
            {
                IncentiveTitle = "Buy this for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentiveReward = "Band t-shirt"
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Incentive price cannot be equal to or less than 0", incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentive_Failure_No_Reward()
        {
            var project = 1;

            var incentive = await inc_.AddIncentive(project, new AddIncentiveOptions()
            {
                IncentiveTitle = "Buy this for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 10
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Incentive reward cannot be null or whitespace", incentive.ErrorText);
        }

        [Fact]
        public async Task AddIncentive_Failure_Project_Not_Found()
        {
            var project = 9999999;

            var incentive = await inc_.AddIncentive(project, new AddIncentiveOptions()
            {
                IncentiveTitle = "Buy this for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 10,
                IncentiveReward = "Band t-shirt"
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
        }

        /// <summary>
        /// Update incentive tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateIncentive_Success()
        {
            var incentive = await inc_.UpdateIncentive(1, new UpdateIncentiveOptions()
            {
                IncentiveTitle = "Buy this Updated for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 15,
                IncentiveReward = "Band t-shirt"
            });

            Assert.NotNull(incentive.Data);
            Assert.Equal(StatusCode.Ok, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task UpdateIncentive_Failure_Null_Options()
        {
            var incentive = await inc_.UpdateIncentive(1, null);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task UpdateIncentive_Failure_Wrong_Id()
        {
            var incentive = await inc_.UpdateIncentive(0, new UpdateIncentiveOptions()
            {
                IncentiveTitle = "Buy this Updated for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 15,
                IncentiveReward = "Band t-shirt"
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Incentive id cannot be equal to or less than 0", 
                incentive.ErrorText);
        }        
        
        [Fact]
        public async Task UpdateIncentive_Failure_Id_Not_Found()
        {
            var incentive = await inc_.UpdateIncentive(999999, new UpdateIncentiveOptions()
            {
                IncentiveTitle = "Buy this Updated for t-shirt",
                IncentiveDescription = "This is the reason to buy this",
                IncentivePrice = 15,
                IncentiveReward = "Band t-shirt"
            });

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
            Assert.Equal("Incentive Id not found in database", 
                incentive.ErrorText);
        }

        /// <summary>
        /// Update incentive tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RemoveUpdate_Success()
        {
            var incentive = await inc_.RemoveIncentive(2);

            Assert.NotNull(incentive.Data);
            Assert.Equal(StatusCode.Ok, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task RemoveUpdate_Failure_Wrong_Id()
        {
            var incentive = await inc_.RemoveIncentive(0);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task RemoveUpdate_Failure_Id_Not_Found()
        {
            var incentive = await inc_.RemoveIncentive(999999);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetIncentiveByProjectId_Success()
        {
            var incentive = await inc_.GetIncentiveByProjectId(1);

            Assert.NotNull(incentive.Data);
            Assert.Equal(StatusCode.Ok, incentive.ErrorCode);
        }       
        
        [Fact]
        public async Task GetIncentiveByProjectId_Failure_Wrong_Id()
        {
            var incentive = await inc_.GetIncentiveByProjectId(0);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task GetIncentiveByProjectId_Failure_Id_Not_Found()
        {
            var incentive = await inc_.GetIncentiveByProjectId(999999);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddIncentiveBacker_Success()
        {

            var project = 1;
            var incentiveId = 3;
            var backer = 2;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.Ok, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Wrong_Project()
        {

            var project = 0;
            var incentiveId = 1;
            var backer = 2;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Project id cannot be equal to or less than 0", 
                incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Wrong_Backer()
        {

            var project = 1;
            var incentiveId = 1;
            var backer = 0;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Backer id cannot be equal to or less than 0", 
                incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Wrong_Incentive()
        {

            var project = 1;
            var incentiveId = 0;
            var backer = 1;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.BadRequest, incentive.ErrorCode);
            Assert.Equal("Incentive id cannot be equal to or less than 0", 
                incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Not_Found_Project()
        {

            var project = 999999;
            var incentiveId = 1;
            var backer = 1;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Not_Found_Backer()
        {

            var project = 1;
            var incentiveId = 1;
            var backer = 999999;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Not_Found_Incentive()
        {

            var project = 1;
            var incentiveId = 999999;
            var backer = 1;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.NotFound, incentive.ErrorCode);
            Assert.Equal("Incentive id not found in database", incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Missmatch_Incentive_Project()
        {

            var project = 2;
            var incentiveId = 1;
            var backer = 1;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.Conflict, incentive.ErrorCode);
            Assert.Equal("Incentive does not belong to this project. Backer not added", 
                incentive.ErrorText);
        }        
        
        [Fact]
        public async Task AddIncentiveBacker_Failure_Missmatch_Creator_Backer()
        {

            var project = 1;
            var incentiveId = 1;
            var backer = 5;

            var incentive = await inc_.AddIncentiveBacker(project, incentiveId, backer);

            Assert.Null(incentive.Data);
            Assert.Equal(StatusCode.Conflict, incentive.ErrorCode);
            Assert.Equal("Creator and backer cannot be the same person. Backer not added", 
                incentive.ErrorText);
        }
    }
}
