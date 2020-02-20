using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.MediaOptions;

namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface IMediaService
    {
        Task<ApiResult<Media>> AddMedia(int projectId, AddMediaOptions options);

        Task<ApiResult<Media>> RemoveMedia(int mediaId);

        Task<ApiResult<IQueryable<Media>>> GetMediaByProjectId(int projectId);

        IQueryable<Media> SearchMedia(SearchMediaOptions options);
    }
}
