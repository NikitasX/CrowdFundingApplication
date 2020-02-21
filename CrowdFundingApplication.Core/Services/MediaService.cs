using System;
using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.MediaOptions;

namespace CrowdFundingApplication.Core.Services
{
    public class MediaService : IMediaService
    {

        private readonly CrowdFundingDbContext context;
        private readonly IProjectService projects;
        private readonly ILoggerService logger;

        public MediaService(
            CrowdFundingDbContext ctx,
            IProjectService pjct,
            ILoggerService lgr)
        {
            context = ctx
                    ?? throw new ArgumentNullException(nameof(ctx));

            projects = pjct
                    ?? throw new ArgumentNullException(nameof(pjct));

            logger = lgr
                    ?? throw new ArgumentNullException(nameof(lgr));
        }

        public async Task<ApiResult<Media>> AddMedia(int projectId, AddMediaOptions options)
        {
            if(projectId <= 0) {
                return new ApiResult<Media>(
                    StatusCode.BadRequest,
                    "Project id cannot be equal to or less than 0");
            }

            if(options == null) {
                return new ApiResult<Media>(
                    StatusCode.BadRequest,
                    $"Media options: \'{nameof(options)}\' cannot be null");
            }

            if(string.IsNullOrWhiteSpace(options.MediaURL)) {
                return new ApiResult<Media>(
                    StatusCode.BadRequest,
                    "Media URL cannot be null or whitespace");
            }            
            
            if(options.MediaType == MediaTypes.Invalid) {
                return new ApiResult<Media>(
                    StatusCode.BadRequest,
                    "Media Type not set (Invalid)");
            }

            var project = await projects.GetProjectById(projectId);

            if(!project.Success) {
                return project.ToResult<Media>();
            }

            var media = new Media()
            {
                Project = project.Data,
                MediaType = options.MediaType,
                MediaURL = options.MediaURL
            };

            context.Add(media);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Changes not saved, media not added.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Media>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, media not added");
            }

            if (success) {
                return ApiResult<Media>.CreateSuccess(media);
            } else {
                return new ApiResult<Media>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, media not added");
            }
        }

        public async Task<ApiResult<Media>> RemoveMedia(int mediaId)
        {
            if(mediaId <= 0) {
                return new ApiResult<Media>(
                    StatusCode.BadRequest,
                    "Media id cannot be equal to or less than 0");
            }

            var media = SearchMedia(new SearchMediaOptions()
            {
                MediaId = mediaId
            }).SingleOrDefault();

            if(media == null) {
                return new ApiResult<Media>(
                    StatusCode.NotFound,
                    "Media id not found in database");
            }

            context.Remove(media);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Media not deleted.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Media>(
                    StatusCode.InternalServerError,
                    $"Media not deleted");
            }

            if (success) {
                return ApiResult<Media>.CreateSuccess(media);
            } else {
                return new ApiResult<Media>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, media not deleted");
            }
        }

        public async Task<ApiResult<IQueryable<Media>>> GetMediaByProjectId(int projectId)
        {
            if(projectId <= 0) {
                return new ApiResult<IQueryable<Media>>(
                    StatusCode.BadRequest,
                    "Project id cannot be equal to or less than 0");
            }

            var project = await projects.GetProjectById(projectId);

            if(!project.Success) {
                return project.ToResult<IQueryable<Media>>();
            }

            return ApiResult<IQueryable<Media>>.CreateSuccess(context
                .Set<Media>()
                .AsQueryable()
                .Where(m => m.Project == project.Data)
                .Take(500));
        }

        public IQueryable<Media> SearchMedia(SearchMediaOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context
                .Set<Media>()
                .AsQueryable();

            if (options.MediaId != 0) {
                query = query
                    .Where(c => c.MediaId == options.MediaId);
            }

            if (options.MediaType != MediaTypes.Invalid) {
                query = query
                    .Where(c => c.MediaType == options.MediaType);
            }

            return query.Take(500);
        }
    }
}
