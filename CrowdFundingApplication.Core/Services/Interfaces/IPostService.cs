using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.PostOptions;

namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface IPostService
    {
        Task<ApiResult<Post>> AddPost(int projectId, int userId,
            AddPostOptions options);        
        
        Task<ApiResult<Post>> UpdatePost(int postId,
            UpdatePostOptions options);        
        
        Task<ApiResult<Post>> RemovePost(int postId);

        ///DIMITRIS CHECK
        Task<ApiResult<IQueryable<Post>>> GetPostByProjectId(int projectId);
    }
}
