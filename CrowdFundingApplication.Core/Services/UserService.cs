using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.User;
using CrowdFundingApplication.Core.Services.Interfaces;

namespace CrowdFundingApplication.Core.Services
{
    public class UserService : IUserServices
    {
        private readonly CrowdFundingDbContext context;
        private readonly ILoggerService logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public UserService(
            CrowdFundingDbContext ctx,
            ILoggerService lgr)
        {
            context = ctx
                    ?? throw new ArgumentNullException(nameof(ctx));

            logger = lgr 
                    ?? throw new ArgumentNullException(nameof(ctx));
        }

        public async Task<ApiResult<User>> AddUser(AddUserOptions options)
        {
            if (options == null) {
                return new ApiResult<User>(
                    StatusCode.BadRequest,
                    $"User options: \'${nameof(options)}\' cannot be null");
            }

            if (string.IsNullOrWhiteSpace(options.UserEmail)) {
                return new ApiResult<User>(
                    StatusCode.BadRequest,
                    "User email cannot be null or whitespace");
            }

            if (string.IsNullOrWhiteSpace(options.UserLastName)) {
                return new ApiResult<User>(
                    StatusCode.BadRequest,
                    "User last name cannot be null or whitespace");
            }

            var exists = await SearchUser(
                new SearchUserOptions()
                {
                    UserEmail = options.UserEmail
                }).AnyAsync();

            if (exists) {
                return new ApiResult<User>(
                    StatusCode.Conflict,
                    "User email already in database");
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

                logger.LogError(
                    StatusCode.InternalServerError.ToString(),
                    $"Changes not saved, user not added.");
                logger.LogInformation(e.ToString());

                return new ApiResult<User>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, user not added");
            }

            if (success) {
                return ApiResult<User>.CreateSuccess(user);
            } else {
                return new ApiResult<User>(
                    StatusCode.InternalServerError,
                    "Something went wrong, user not added");
            }
        }

        public async Task<ApiResult<User>> UpdateUser(int userId, UpdateUserOptions options)
        {
            if(userId <= 0) {
                return new ApiResult<User>(
                    StatusCode.BadRequest, 
                    $"{userId} cannot be equal to or less than 0");
            }

            if(options == null) {
                return new ApiResult<User>(
                    StatusCode.BadRequest,
                    $"{options} cannot null");
            }

            var user = await SearchUser(new SearchUserOptions()
            {
                UserId = userId
            }).SingleOrDefaultAsync();

            if(user == null) {
                return new ApiResult<User>(StatusCode.NotFound,
                    $"User Id not found in database");
            }

            if (!string.IsNullOrWhiteSpace(options.UserEmail)) {
                var emailCheck = SearchUser(new SearchUserOptions()
                {
                    UserEmail = options.UserEmail
                }).SingleOrDefault();

                if(emailCheck == null) {
                    user.UserEmail = options.UserEmail;
                } else {
                    return new ApiResult<User>(
                        StatusCode.Conflict,
                        "Email already found in database");
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

                logger.LogError(
                    StatusCode.InternalServerError.ToString(),
                    $"Changes not saved, user not updated.");
                logger.LogInformation(e.ToString());

                return new ApiResult<User>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, user not updated. {e}");
            }

            if (success) {
                return ApiResult<User>.CreateSuccess(user);
            } else {
                return new ApiResult<User>(
                    StatusCode.InternalServerError,
                    "Something went wrong, user not updated");
            }
        }

        public async Task<ApiResult<User>> GetUserById(int userId)
        {
            if(userId <= 0) {
                return new ApiResult<User>(
                    StatusCode.BadRequest,
                    $"{userId} cannot be equal to or less than 0");
            }

            var user = await SearchUser(new SearchUserOptions()
            {
                UserId = userId
            }).SingleOrDefaultAsync();

            if (user == null) {
                return new ApiResult<User>(
                    StatusCode.NotFound, 
                    $"User not found in database");
            }

            return ApiResult<User>.CreateSuccess(user);
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
