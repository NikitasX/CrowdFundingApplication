using Xunit;
using Autofac;
using System.Threading.Tasks;
using CrowdFundingApplication.Core;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.PostOptions;

namespace CrowdFundingApplication.Tests
{
    public class PostServiceTests : IClassFixture<CrowdFundingApplicationFixture>
    {

        private readonly IPostService post_;
        private readonly IProjectService pjct_;
        private readonly IUserService usr_;
        private readonly CrowdFundingDbContext context_;


        public PostServiceTests(CrowdFundingApplicationFixture fixture)
        {
            context_ = fixture.DbContext;
            pjct_ = fixture.Container.Resolve<IProjectService>();
            post_ = fixture.Container.Resolve<IPostService>();
            usr_ = fixture.Container.Resolve<IUserService>();
        }

        [Fact]
        public async Task AddPost_Success()
        {
            var project = 1;
            var user = 1;

            var post = await post_.AddPost(project, user, new AddPostOptions()
            {
                PostTitle = "My post status update",
                PostExcerpt = "My post status update excerpt"
            });

            Assert.NotNull(post.Data);
            Assert.Equal(StatusCode.Ok, post.ErrorCode);
        }

        /// <summary>
        /// Add post service tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddPost_Failure_Empty_Options()
        {
            var project = 1;
            var user = 1;

            var post = await post_.AddPost(project, user, null);

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
        }

        [Fact]
        public async Task AddPost_Failure_Wrong_User_Id()
        {
            var project = 1;
            var user = 0;

            var post = await post_.AddPost(project, user, new AddPostOptions()
            {
                PostTitle = "My post status update",
                PostExcerpt = "My post status update excerpt"
            });

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
            Assert.Equal("User id cannot be equal to or less than 0", post.ErrorText);
        } 
        
        [Fact]
        public async Task AddPost_Failure_Wrong_Project_Id()
        {
            var project = 0;
            var user = 1;

            var post = await post_.AddPost(project, user, new AddPostOptions()
            {
                PostTitle = "My post status update",
                PostExcerpt = "My post status update excerpt"
            });

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
            Assert.Equal("Project id cannot be equal to or less than 0", post.ErrorText);
        }        
        
        [Fact]
        public async Task AddPost_Failure_Wrong_Title()
        {
            var project = 1;
            var user = 1;

            var post = await post_.AddPost(project, user, new AddPostOptions()
            {
                PostExcerpt = "My post status update excerpt"
            });

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
            Assert.Equal("Post title cannot be null or whitespace", post.ErrorText);
        }

        /// <summary>
        /// Get post by id tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetPostById_Success()
        {
            var post = await post_.GetPostByProjectId(1);

            Assert.NotNull(post.Data);
            Assert.Equal(StatusCode.Ok, post.ErrorCode);
        }        
        
        [Fact]
        public async Task GetPostById_Wrong_Id()
        {
            var post = await post_.GetPostByProjectId(0);

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
            Assert.Equal("Project id cannot be equal to or less than 0", post.ErrorText);
        }    
        
        [Fact]
        public async Task GetPostById_Not_Found_Id()
        {
            var post = await post_.GetPostByProjectId(999999);

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.NotFound, post.ErrorCode);
        }  
        
        /// <summary>
        /// Remove post tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RemovePost_Success()
        {
            var post = await post_.RemovePost(1);

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.Ok, post.ErrorCode);
        }        
        
        [Fact]
        public async Task RemovePost_Failure_Wrong_Id()
        {
            var post = await post_.RemovePost(0);

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
            Assert.Equal("Post id cannot be equal to or less than 0", post.ErrorText);
        }        
        
        [Fact]
        public async Task RemovePost_Failure_Id_Not_Found()
        {
            var post = await post_.RemovePost(999999);

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.NotFound, post.ErrorCode);
            Assert.Equal("Post id not found in database", post.ErrorText);
        }

        /// <summary>
        /// Update post tests
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdatePost_Success()
        {
            var post = await post_.UpdatePost(2, new UpdatePostOptions()
            {
                PostTitle = "My UPDATED post title"
            });

            Assert.NotNull(post.Data);
            Assert.Equal(StatusCode.Ok, post.ErrorCode);
        }        
        
        [Fact]
        public async Task UpdatePost_Failure_Wrong_Id()
        {
            var post = await post_.UpdatePost(0, new UpdatePostOptions()
            {
                PostTitle = "My UPDATED wrong post title"
            });

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.BadRequest, post.ErrorCode);
            Assert.Equal("Post id cannot be equal to or less than 0", post.ErrorText);
        }        
        
        [Fact]
        public async Task UpdatePost_Failure_Not_Found_Id()
        {
            var post = await post_.UpdatePost(999999, new UpdatePostOptions()
            {
                PostTitle = "My UPDATED wrong post title"
            });

            Assert.Null(post.Data);
            Assert.Equal(StatusCode.NotFound, post.ErrorCode);
            Assert.Equal("Post id not found in database", post.ErrorText);
        }
    }
}
