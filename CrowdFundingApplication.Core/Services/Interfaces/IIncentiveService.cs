using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.IncentiveOptions;

namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface IIncentiveService
    {
        Task<ApiResult<Incentive>> AddIncentive(int projectId, AddIncentiveOptions options);

        Task<ApiResult<Incentive>> UpdateIncentive(int incentiveId, UpdateIncentiveOptions options);

        Task<ApiResult<Incentive>> RemoveIncentive(int incentiveId);

        Task<ApiResult<IQueryable<Incentive>>> GetIncentiveByProjectId(int projectId);

        IQueryable<Incentive> SearchIncentive(SearchIncentiveOptions options);

        Task<ApiResult<Incentive>> AddIncentiveBacker
            (int projectId, int incentiveId, int backerId);        
        
        Task<ApiResult<Incentive>> RemoveIncentiveBacker
            (int projectId, int incentiveId, int backerId);
    }
}
