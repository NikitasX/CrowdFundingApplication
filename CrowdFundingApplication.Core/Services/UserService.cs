using System;
using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.User;
using Microsoft.EntityFrameworkCore;

namespace CrowdFundingApplication.Core.Services
{
    public class UserService : IUserServices
    {
        private readonly CrowdFundingDbContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public UserService(CrowdFundingDbContext ctx)
        {
            context = ctx
                    ?? throw new ArgumentNullException(nameof(ctx));
        }

        public async Task<ApiResult<User>> AddUser(AddUserOptions options)
        {
            if(options == null) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = "User options cannot be null"
                };
            }

            if(string.IsNullOrWhiteSpace(options.UserEmail)) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = "User email cannot be null or whitespace"
                };
            }

            if (string.IsNullOrWhiteSpace(options.UserLastName)) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = "User last name cannot be null or whitespace"
                };
            }

            var exists = await SearchUser(
                new SearchUserOptions()
                {
                    UserEmail = options.UserEmail
                }).AnyAsync();

            if (exists) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.Conflict,
                    ErrorText = "User email already in database"
                };
            }

            var user = new User()
            {
                UserVat = options.UserVat,
                UserEmail = options.UserEmail,
                UserPhone = options.UserPhone,
                UserDateCreated = DateTime.UtcNow,
                UserLastName = options.UserLastName,
                UserFirstName = options.UserFirstName
            };

            context.Add(user);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.InternalServerError,
                    ErrorText = $"Something went wrong, user not added {e}"
                };
            }

            if (success) {
                return ApiResult<User>.CreateSuccess(user);
            } else {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.InternalServerError,
                    ErrorText = $"Something went wrong, user not added"
                };
            }
        }

        public async Task<ApiResult<User>> UpdateUser(int userId, UpdateUserOptions options)
        {
            if(userId <= 0) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"{userId} cannot be equal to or less than 0"
                };
            }

            if(options == null) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"{options} cannot null"
                };
            }

            var user = await SearchUser(new SearchUserOptions()
            {
                UserId = userId
            }).SingleOrDefaultAsync();

            if(user == null) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.NotFound,
                    ErrorText = $"User Id not found in database"
                };
            }

            if (!string.IsNullOrWhiteSpace(options.UserEmail)) {
                var emailCheck = SearchUser(new SearchUserOptions()
                {
                    UserEmail = options.UserEmail
                }).SingleOrDefault();

                if(emailCheck == null) {
                    user.UserEmail = options.UserEmail;
                } else {
                    return new ApiResult<User>()
                    {
                        ErrorCode = StatusCode.Conflict,
                        ErrorText = $"Email already found in database"
                    };
                }
            }

            if (!string.IsNullOrWhiteSpace(options.UserFirstName)) {
                user.UserFirstName = options.UserFirstName;
            }            
            
            if (!string.IsNullOrWhiteSpace(options.UserLastName)) {
                user.UserLastName = options.UserLastName;
            }            
            
            if (!string.IsNullOrWhiteSpace(options.UserPhone)) {
                user.UserPhone = options.UserPhone;
            }            
            
            if (!string.IsNullOrWhiteSpace(options.UserVat)) {
                user.UserVat = options.UserVat;
            }

            context.Update(user);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.InternalServerError,
                    ErrorText = $"Something went wrong, user not updated. {e}"
                };
            }

            if (success) {
                return ApiResult<User>.CreateSuccess(user);
            } else {
                return new ApiResult<User>()
                {
                    ErrorCode = StatusCode.InternalServerError,
                    ErrorText = "Something went wrong, user not updated"
                };
            }
        }

        public IQueryable<User> SearchUser(SearchUserOptions options)
        {
            if(options == null) {
                return default;
            }

            var query = context
                .Set<User>()
                .AsQueryable();

            if (options.UserId != null) {
                return query
                    .Where(s => s.UserId == options.UserId);
            }

            if (!string.IsNullOrWhiteSpace(options.UserVat)) {
                query = query
                    .Where(s => s.UserVat == options.UserVat);
            }

            if (!string.IsNullOrWhiteSpace(options.UserEmail)) {
                query = query
                    .Where(s => s.UserEmail == options.UserEmail);
            }

            if (options.UserDateCreatedFrom != null) {
                query = query
                    .Where(s => s.UserDateCreated > options.UserDateCreatedFrom);
            }

            if (options.UserDateCreatedTo != null) {
                query = query
                    .Where(s => s.UserDateCreated < options.UserDateCreatedTo);
            }

            return query
                .Take(500);
        }
    }
}
