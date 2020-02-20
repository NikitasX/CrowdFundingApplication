using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.ProjectOptions;

namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResult<Project>> AddProject(int userId, AddProjectOptions options);

        Task<ApiResult<Project>> UpdateProject(int projectId, 
            UpdateProjectOptions options);

        Task<ApiResult<Project>> GetProjectById(int projectId);

        IQueryable<Project> SearchProject(SearchProjectOptions options);
    }
   
}
