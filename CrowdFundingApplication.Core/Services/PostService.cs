using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.PostOptions;


namespace CrowdFundingApplication.Core.Services
{
    public class PostService : IPostService
    {

        private readonly CrowdFundingDbContext context;
        private readonly IUserService users;
        private readonly IProjectService projects;
        private readonly ILoggerService logger;

        public PostService(
            IUserService usr,
            IProjectService pjct,
            ILoggerService lgr,
            CrowdFundingDbContext ctx)
        {
            context = ctx ??
                throw new ArgumentNullException(nameof(ctx));

            users = usr ??
                throw new ArgumentNullException(nameof(usr));            
            
            projects = pjct ??
                throw new ArgumentNullException(nameof(pjct));

            logger = lgr ??
                throw new ArgumentNullException(nameof(lgr));
        }

        public async Task<ApiResult<Post>> AddPost(int projectId, int userId, 
            AddPostOptions options)
        {
            if(projectId <= 0) {
                return new ApiResult<Post>(
                    StatusCode.BadRequest,
                    "Project id cannot be equal to or less than 0");
            }            
            
            if(userId <= 0) {
                return new ApiResult<Post>(
                    StatusCode.BadRequest,
                    "User id cannot be equal to or less than 0");
            }            
            
            if(options == null) {
                return new ApiResult<Post>(
                    StatusCode.BadRequest,
                    $"Post options: \'{nameof(options)}\' cannot be null");
            }            
            
            if(string.IsNullOrWhiteSpace(options.PostTitle)) {
                return new ApiResult<Post>(
                    StatusCode.BadRequest,
                    $"Post title cannot be null or whitespace");
            }

            var project = await projects.GetProjectById(projectId);

            if(!project.Success) {
                return new ApiResult<Post>(
                    StatusCode.NotFound,
                    $"Project Id not found in database");
            }           
            
            var user = await users.GetUserById(userId);

            if(!user.Success) {
                return new ApiResult<Post>(
                    StatusCode.NotFound,
                    $"User Id not found in database");
            }

            var post = new Post() 
            {
                Project = project.Data,
                User = user.Data,
                PostTitle = options.PostTitle,
                PostExcerpt = options.PostExcerpt
            };

            context.Add(post);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Changes not saved, post not added.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Post>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, post not added");
            }

            if (success) {
                return ApiResult<Post>.CreateSuccess(post);
            } else {
                return new ApiResult<Post>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, post not added");
            }
        }

        public async Task<ApiResult<IQueryable<Post>>> GetPostByProjectId(int projectId)
        {
            if(projectId <= 0) {
                return new ApiResult<IQueryable<Post>>(
                    StatusCode.BadRequest,
                    "Project id cannot be equal to or less than 0");
            }

            var project = await projects.GetProjectById(projectId);

            if(!project.Success) {
                return new ApiResult<IQueryable<Post>>(
                    StatusCode.NotFound,
                    "Project not found in database");
            }

            var query = context
                .Set<Post>()
                .Where(p => p.Project == project.Data)
                .AsQueryable()
                .Take(500);

            return ApiResult<IQueryable<Post>>
                .CreateSuccess(query);
        }

        public async Task<ApiResult<Post>> RemovePost(int postId)
        {
            if (postId <= 0) {
                return new ApiResult<Post>(
                    StatusCode.BadRequest,
                    "Post id cannot be equal to or less than 0");
            }

            var post = await context
                .Set<Post>()
                .Where(p => p.PostId == postId)
                .SingleOrDefaultAsync();

            if (post == null) {
                return new ApiResult<Post>(
                    StatusCode.NotFound,
                    "Post id not found in database");
            }

            context.Remove(post);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Post not deleted.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Post>(
                    StatusCode.InternalServerError,
                    $"Post not deleted");
            }

            if (success) {
                return ApiResult<Post>.CreateSuccess(post);
            } else {
                return new ApiResult<Post>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, post not deleted");
            }
        }

        public async Task<ApiResult<Post>> UpdatePost(int postId, 
            UpdatePostOptions options)
        {
            if (postId <= 0) {
                return new ApiResult<Post>(
                    StatusCode.BadRequest,
                    "Post id cannot be equal to or less than 0");
            }

            var post = await context
                .Set<Post>()
                .Where(p => p.PostId == postId)
                .SingleOrDefaultAsync();

            if (post == null) {
                return new ApiResult<Post>(
                    StatusCode.NotFound,
                    "Post id not found in database");
            }

            if(!string.IsNullOrWhiteSpace(options.PostTitle)) {
                post.PostTitle = options.PostTitle;
            }            
            
            if(!string.IsNullOrWhiteSpace(options.PostExcerpt)) {
                post.PostExcerpt = options.PostExcerpt;
            }

            context.Update(post);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Post not updated.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Post>(
                    StatusCode.InternalServerError,
                    $"Post not updated");
            }

            if (success) {
                return ApiResult<Post>.CreateSuccess(post);
            } else {
                return new ApiResult<Post>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, post not updated");
            }
        }
    }
}
