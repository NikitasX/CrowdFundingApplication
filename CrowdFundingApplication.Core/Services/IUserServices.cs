using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.User;

namespace CrowdFundingApplication.Core.Services
{
    public interface IUserServices
    {
        Task<ApiResult<User>> AddUser(AddUserOptions options);

        Task<ApiResult<User>> UpdateUser(int userId, UpdateUserOptions options);

        IQueryable<User> SearchUser(SearchUserOptions options);
    }
}
