using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrowdFundingApplication.Core;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.ProjectOptions;

namespace CrowdFundingApplication.Core.Services
{
   public interface IProjectService
    {
        bool AddProject(int userId, AddProjectOptions options);

        IQueryable<Model.Project> SearchProject(
            SearchProjectOptions options);

        bool UpdateProject(int projectid,
            UpdateProjectOptions options);

        public Project GetProjectById(int projectid);
    }
   
}
