using System;
using System.Linq;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.ProjectOptions;

namespace CrowdFundingApplication.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CrowdFundingDbContext context;
        private readonly IUserServices users;

        public ProjectService(
            IUserServices usr,
            CrowdFundingDbContext ctx)
        {
            context = ctx ??
                throw new ArgumentNullException(nameof(ctx));            
            
            users = usr ??
                throw new ArgumentNullException(nameof(usr));
        }
        public bool AddProject(int userId, AddProjectOptions options)
        {

            if (options == null) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(options.ProjectTitle) ||
               string.IsNullOrWhiteSpace(options.ProjectDescription)) {
                return false;
            }

            if (options.ProjectFinancialGoal <= 0
                || options.ProjectCapitalAcquired < 0) {
                return false;
            }
            if (options.ProjectCategory ==
              Model.ProjectCategory.Invalid) {
                return false;
            }
            if (options.ProjectDateExpiring == null) {
                return false;
            }

            var user = users.SearchUser(new Model.Options.User.SearchUserOptions()
            {
                UserId = userId
            }).SingleOrDefault();

            if(user == null) {
                return false;
            }

            var project = new Project()
            {
                ProjectUser = user,
                ProjectDescription = options.ProjectDescription,
                ProjectTitle = options.ProjectTitle,
                ProjectFinancialGoal = options.ProjectFinancialGoal,
                ProjectCategory = options.ProjectCategory,
                ProjectDateExpiring = options.ProjectDateExpiring,
                ProjectCapitalAcquired = options.ProjectCapitalAcquired
            };

            context.Add(project);
            
            context.SaveChanges();

            return true;
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
                query = query.Where(c =>
                    c.ProjectTitle == options.ProjectTitle);
            }

            if (options.ProjectId != 0) {
                query = query.Where(c =>
                    c.ProjectId == options.ProjectId);
            }

            if (options.ProjectCategory != 0) {
                query = query.Where(c =>
                   c.ProjectCategory == options.ProjectCategory);
            }
            if (options.ProjectDateExpiring != null) {
                query = query.Where(c =>
                   c.ProjectDateExpiring == options.ProjectDateExpiring);
            }

            return query.Take(500);
        }

        public bool UpdateProject(int projectid, UpdateProjectOptions options)
        {

           
            if (options == null) {
                return false;
            }
            
         
            var project = GetProjectById(projectid);


            if (project == null) {
                return false;
            }

            if (!string.IsNullOrWhiteSpace
                (options.ProjectTitle)) {
                project.ProjectTitle = options.ProjectTitle;
            }

            if (options.ProjectFinancialGoal != 0) {
                if (options.ProjectFinancialGoal <= 0) {
                    return false;
                } else {
                    project.ProjectFinancialGoal = options.ProjectFinancialGoal;
                }
            }

            if (options.ProjectCapitalAcquired != 0) {
                if (options.ProjectCapitalAcquired <= 0) {
                    return false;
                } else {
                    project.ProjectCapitalAcquired = options.ProjectCapitalAcquired;
                }
            }

            if (!string.IsNullOrWhiteSpace
                (options.ProjectDescription)) {
                project.ProjectDescription = options.ProjectDescription;
            }

            if (options.ProjectCategory !=
              ProjectCategory.Invalid) {
                project.ProjectCategory = options.ProjectCategory;
            }

            if (options.ProjectDateExpiring != null) {
                project.ProjectDateExpiring = options.ProjectDateExpiring;
            }

            return true;
        }
        public Project GetProjectById(int projectid)
        {
            if (projectid == 0) {
                return null;
            }
            return context
                .Set<Project>()
                .SingleOrDefault(p => p.ProjectId == projectid);
        }

    }
   
}
