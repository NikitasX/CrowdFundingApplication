using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.User;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.ProjectOptions;

namespace CrowdFundingApplication.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CrowdFundingDbContext context;
        private readonly IUserService users;
        private readonly ILoggerService logger;

        public ProjectService(
            IUserService usr,
            ILoggerService lgr,
            CrowdFundingDbContext ctx)
        {
            context = ctx ??
                throw new ArgumentNullException(nameof(ctx));            
            
            users = usr ??
                throw new ArgumentNullException(nameof(usr));

            logger = lgr ??
                throw new ArgumentNullException(nameof(lgr));
        }
        
        public async Task<ApiResult<Project>> AddProject(int userId, AddProjectOptions options)
        {

            if (options == null) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    $"Project options: \'{nameof(options)}\' cannot be null");
            }

            if (userId <= 0) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    "User id cannot be equal to or less than 0");
            }

            if (string.IsNullOrWhiteSpace(options.ProjectTitle)) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    "Project title cannot be null or whitespace");
            }

            if (options.ProjectFinancialGoal <= 0) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    "Financial goal cannot be equal to or less than 0");
            }

            if (options.ProjectCategory ==
              Model.ProjectCategory.Invalid) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    "Project category not set (Invalid)");
            }

            if (options.ProjectDateExpiring == default(DateTimeOffset)) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    "Project expiration date cannot be null");
            }

            var user = await users.SearchUser(new SearchUserOptions()
            {
                UserId = userId
            }).SingleOrDefaultAsync();

            if (user == null) {
                return new ApiResult<Project>(
                    StatusCode.NotFound,
                    "User id (from project) not found in database");
            }

            var project = new Project()
            {
                User = user,
                ProjectDescription = options.ProjectDescription,
                ProjectTitle = options.ProjectTitle,
                ProjectFinancialGoal = options.ProjectFinancialGoal,
                ProjectCategory = options.ProjectCategory,
                ProjectDateExpiring = options.ProjectDateExpiring
            };

            context.Add(project);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Changes not saved, project not added.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Project>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, project not added");
            }

            if (success) {
                return ApiResult<Project>.CreateSuccess(project);
            } else {
                return new ApiResult<Project>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, project not added");
            }
        }

        public async Task<ApiResult<Project>> UpdateProject(int projectId, UpdateProjectOptions options)
        {

            if (projectId <= 0) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    $"{projectId} cannot be equal to or less than 0");
            }

            if (options == null) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    $"Project options: \'{nameof(options)}\' cannot be null");
            }
            
            var project = await SearchProject(new SearchProjectOptions()
            {
                ProjectId = projectId
            }).SingleOrDefaultAsync();


            if (project == null) {
                return new ApiResult<Project>(
                    StatusCode.NotFound,
                    $"Project Id not found in database");
            }

            if (!string.IsNullOrWhiteSpace(options.ProjectTitle)) {
                project.ProjectTitle = options.ProjectTitle;
            }

            if(options.ProjectFinancialGoal > 0) {
                project.ProjectFinancialGoal = options.ProjectFinancialGoal;
            }

            if (options.ProjectCapitalAcquired > 0) {
                project.ProjectCapitalAcquired = options.ProjectCapitalAcquired;
            }

            if (!string.IsNullOrWhiteSpace(options.ProjectDescription)) {
                project.ProjectDescription = options.ProjectDescription;
            }

            if (options.ProjectCategory != ProjectCategory.Invalid) {
                project.ProjectCategory = options.ProjectCategory;
            }

            if (options.ProjectDateExpiring != default(DateTimeOffset)) {
                project.ProjectDateExpiring = options.ProjectDateExpiring;
            }

            context.Update(project);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(), 
                    $"Changes not saved, project not updated.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Project>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, project not updated.");
            }

            if (success) {
                return ApiResult<Project>.CreateSuccess(project);
            } else {
                return new ApiResult<Project>(
                    StatusCode.InternalServerError,
                    "Something went wrong, project not updated");
            }
        }

        public async Task<ApiResult<Project>> GetProjectById(int projectId)
        {
            if (projectId <= 0) {
                return new ApiResult<Project>(
                    StatusCode.BadRequest,
                    $"{projectId} cannot be equal to or less than 0");
            }

            var project = await SearchProject(new SearchProjectOptions()
            {
                ProjectId = projectId
            }).SingleOrDefaultAsync();

            if (project == null) {
                return new ApiResult<Project>(
                    StatusCode.NotFound,
                    $"Project not found in database");
            }

            return ApiResult<Project>.CreateSuccess(project);
        }

        public IQueryable<Project> SearchProject(SearchProjectOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context
                .Set<Project>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.ProjectTitle)) {
                query = query
                    .Where(c => c.ProjectTitle.Contains(options.ProjectTitle));
            }

            if (options.ProjectId != 0) {
                query = query
                    .Where(c => c.ProjectId == options.ProjectId);
            }

            if (options.ProjectCategory != 0) {
                query = query
                    .Where(c => c.ProjectCategory == options.ProjectCategory);
            }

            if (options.ProjectDateExpiringFrom != default(DateTimeOffset)) {
                query = query.Where(c =>
                   c.ProjectDateExpiring > options.ProjectDateExpiringFrom);
            }            
            
            if (options.ProjectDateExpiringTo != default(DateTimeOffset)) {
                query = query.Where(c =>
                   c.ProjectDateExpiring < options.ProjectDateExpiringTo);
            }

            return query
                .Include(m => m.ProjectMedia)
                .Include(p => p.ProjectPosts)
                .Include(i => i.ProjectIncentives)
                .Take(500);
        }
    }
}
