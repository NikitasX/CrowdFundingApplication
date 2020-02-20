using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.User;

namespace CrowdFundingApplication.Core.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApiResult<User>> AddUser(AddUserOptions options);

        Task<ApiResult<User>> UpdateUser(int userId, UpdateUserOptions options);

        Task<ApiResult<User>> GetUserById(int userId);

        IQueryable<User> SearchUser(SearchUserOptions options);
    }
}
