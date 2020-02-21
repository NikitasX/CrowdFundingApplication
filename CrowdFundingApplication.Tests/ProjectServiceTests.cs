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
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.ProjectOptions;

namespace CrowdFundingApplication.Tests
{
    public class ProjectServiceTests : IClassFixture<CrowdFundingApplicationFixture>
    {

        private readonly IUserService ursv_;
        private readonly IProjectService pjct_;
        private readonly CrowdFundingDbContext context_;

        public static Random GenerateRandomNumber = new Random();

        public ProjectServiceTests(CrowdFundingApplicationFixture fixture)
        {
            context_ = fixture.DbContext;
            pjct_ = fixture.Container.Resolve<IProjectService>();
            ursv_ = fixture.Container.Resolve<IUserService>();
        }

        /// <summary>
        /// Add Project Tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddProject_Success()
        {
            int num = GenerateRandomNumber.Next(1000);
            int num2 = GenerateRandomNumber.Next(10000);

            var userOptions = new AddUserOptions()
            {
                UserEmail = $"{num}{num2}@gmail.com",
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhone = "+30XXXXXXXXXX",
                UserVat = "XXXXXXXXX"
            };

            var user = await ursv_.AddUser(userOptions);

            Assert.Equal(StatusCode.Ok, user.ErrorCode);

            var project = await pjct_.AddProject(user.Data.UserId, new AddProjectOptions()
            {
                ProjectTitle = "My test crowd funding project",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 300000M,
                ProjectCategory = ProjectCategory.Technology,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "25/02/2020", 
                    "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture)
            });

            Assert.NotNull(project.Data);
            Assert.Equal(StatusCode.Ok, project.ErrorCode);
        }

        [Fact]
        public async Task AddProject_Failure_Empty_Options()
        {
            var project = await pjct_.AddProject(1, null);

            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
        }        
        
        [Fact]
        public async Task AddProject_Failure_Wrong_User_Id()
        {
            var project = await pjct_.AddProject(0, new AddProjectOptions()
            {
                ProjectTitle = "My test crowd funding project",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 300000M,
                ProjectCategory = ProjectCategory.Technology,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "25/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
            Assert.Equal("User id cannot be equal to or less than 0", project.ErrorText);
        }          
        
        [Fact]
        public async Task AddProject_Failure_User_Id_Not_Found()
        {
            var project = await pjct_.AddProject(999999, new AddProjectOptions()
            {
                ProjectTitle = "My test crowd funding project",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 300000M,
                ProjectCategory = ProjectCategory.Technology,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "25/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Equal(StatusCode.NotFound, project.ErrorCode);
            Assert.Equal("User id (from project) not found in database", project.ErrorText);
        }        
        
        [Fact]
        public async Task AddProject_Failure_No_Project_Title()
        {
            var project = await pjct_.AddProject(1, new AddProjectOptions()
            {
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 300000M,
                ProjectCategory = ProjectCategory.Technology,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "25/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
            Assert.Equal("Project title cannot be null or whitespace", project.ErrorText);
        }        
        
        [Fact]
        public async Task AddProject_Failure_No_Financial_Goal()
        {
            var project = await pjct_.AddProject(1, new AddProjectOptions()
            {
                ProjectTitle = "My test crowd funding project",
                ProjectDescription = "Some crowd funding project test text",
                ProjectCategory = ProjectCategory.Technology,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "25/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
            Assert.Equal("Financial goal cannot be equal to or less than 0", project.ErrorText);
        }

        [Fact]
        public async Task AddProject_Failure_No_Category()
        {
            var project = await pjct_.AddProject(1, new AddProjectOptions()
            {
                ProjectTitle = "My test crowd funding project",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 300000M,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "25/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
            Assert.Equal("Project category not set (Invalid)", project.ErrorText);
        }        
        
        [Fact]
        public async Task AddProject_Failure_No_Expire_Date()
        {
            var project = await pjct_.AddProject(1, new AddProjectOptions()
            {
                ProjectTitle = "My test crowd funding project",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 300000M,
                ProjectCategory = ProjectCategory.Technology
            });

            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
            Assert.Equal("Project expiration date cannot be null", project.ErrorText);
        }

        /// <summary>
        /// Update project tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateProject_Success()
        {
            var project = await pjct_.UpdateProject(1, new UpdateProjectOptions()
            {
                ProjectTitle = "My test crowd funding project Updated",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 350000M,
                ProjectCategory = ProjectCategory.Publishing,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "26/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.NotNull(project.Data);
            Assert.Equal(StatusCode.Ok, project.ErrorCode);
            Assert.Equal("My test crowd funding project Updated", project.Data.ProjectTitle);
            Assert.Equal(350000M, project.Data.ProjectFinancialGoal);
            Assert.Equal(ProjectCategory.Publishing, project.Data.ProjectCategory);
        }

        [Fact]
        public async Task UpdateProject_Failure_Null_Options()
        {
            var project = await pjct_.UpdateProject(1, null);

            Assert.Null(project.Data);
            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
        }

        [Fact]
        public async Task UpdateProject_Failure_Wrong_Proj_Id()
        {
            var project = await pjct_.UpdateProject(0, new UpdateProjectOptions()
            {
                ProjectTitle = "My test crowd funding project Updated",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 350000M,
                ProjectCategory = ProjectCategory.Publishing,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "26/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Null(project.Data);
            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
        }             
        
        [Fact]
        public async Task UpdateProject_Failure_Proj_Id_Not_Found()
        {
            var project = await pjct_.UpdateProject(999999, new UpdateProjectOptions()
            {
                ProjectTitle = "My test crowd funding project Updated",
                ProjectDescription = "Some crowd funding project test text",
                ProjectFinancialGoal = 350000M,
                ProjectCategory = ProjectCategory.Publishing,
                ProjectDateExpiring = DateTimeOffset.ParseExact(
                    "26/02/2020",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture)
            });

            Assert.Null(project.Data);
            Assert.Equal(StatusCode.NotFound, project.ErrorCode);
        }

        /// <summary>
        /// GetProjectById tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProjectById_Success()
        {
            var project = await pjct_.GetProjectById(1);

            Assert.NotNull(project.Data);
            Assert.Equal(StatusCode.Ok, project.ErrorCode);
        }        
        
        [Fact]
        public async Task GetProjectById_Wrong_Id()
        {
            var project = await pjct_.GetProjectById(0);

            Assert.Null(project.Data);
            Assert.Equal(StatusCode.BadRequest, project.ErrorCode);
        }        
        
        [Fact]
        public async Task GetProjectById_Not_Found_Id()
        {
            var project = await pjct_.GetProjectById(999999);

            Assert.Null(project.Data);
            Assert.Equal(StatusCode.NotFound, project.ErrorCode);
        }

        /// <summary>
        /// Search Project tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void SearchProject_Success()
        {
            var project = pjct_.SearchProject(new SearchProjectOptions()
            {
                ProjectCategory = ProjectCategory.Technology
            }).ToList();

            Assert.NotNull(project);
        }

        [Fact]
        public void SearchProject_Success_FromDate()
        {
            var project = pjct_.SearchProject(new SearchProjectOptions() { 
                ProjectDateExpiringFrom = DateTimeOffset.ParseExact(
                    "21/02/2020", 
                    "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture)
            }).ToList();

            Assert.NotNull(project);
        }

        [Fact]
        public void SearchProject_Null_Options()
        {
            var project = pjct_.SearchProject(null);

            Assert.Null(project);
        }        
    }
}
