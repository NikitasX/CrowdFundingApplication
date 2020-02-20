using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model.Options.User;
using CrowdFundingApplication.Core.Services;
using Xunit;

namespace CrowdFundingApplication.Tests
{
    public class UserServiceTests : IClassFixture<CrowdFundingApplicationFixture>
    {

        private readonly IUserServices ursv_;

        private readonly CrowdFundingDbContext context_;

        public static Random GenerateRandomNumber = new Random();
        /// <summary>
        /// Constructor
        /// </summary>
        public UserServiceTests(CrowdFundingApplicationFixture fixture)
        {
            context_ = fixture.DbContext;
            ursv_ = fixture.Container.Resolve<IUserServices>();
        }

        /// <summary>
        /// Add User Tests
        /// </summary>
        [Fact]
        public async Task AddUser_Success()
        {
            int num = GenerateRandomNumber.Next(1000);
            int num2 = GenerateRandomNumber.Next(10000);

            var userOptions = new AddUserOptions()
            {
                UserEmail = $"{num}{num2}@gmail.com",
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhone = "+30XXXXXXXXXX",
                UserVat = "XXXXXXXXX"
            };

            var newUser = await ursv_.AddUser(userOptions);

            Assert.Equal(Core.StatusCode.Ok, newUser.ErrorCode);

            var ensureCorrectEntry = ursv_.SearchUser(new SearchUserOptions()
            {
                UserEmail = userOptions.UserEmail
            });

            Assert.NotNull(ensureCorrectEntry);
        }

        [Fact]
        public async Task AddUser_Failure_Empty_Options()
        {
            var newUser = await ursv_.AddUser(null);            

            Assert.Equal(Core.StatusCode.BadRequest, newUser.ErrorCode);
        }        
        
        [Fact]
        public async Task AddUser_Failure_Empty_Email()
        {
            var userOptions = new AddUserOptions()
            {
                UserFirstName = "John",
                UserLastName = "Doe",
                UserPhone = "+30XXXXXXXXXX",
                UserVat = "XXXXXXXXX"
            };

            var newUser = await ursv_.AddUser(userOptions);

            Assert.Equal(Core.StatusCode.BadRequest, newUser.ErrorCode);
        }

        [Fact]
        public async Task AddUser_Failure_Empty_LastName()
        {
            int num = GenerateRandomNumber.Next(1000);
            int num2 = GenerateRandomNumber.Next(10000);

            var userOptions = new AddUserOptions()
            {
                UserEmail = $"{num}{num2}@gmail.com",
                UserFirstName = "John",
                UserPhone = "+30XXXXXXXXXX",
                UserVat = "XXXXXXXXX"
            };

            var newUser = await ursv_.AddUser(userOptions);

            Assert.Equal(Core.StatusCode.BadRequest, newUser.ErrorCode);
        }

        /// <summary>
        /// Update User Tests
        /// </summary>
        [Fact]
        public async Task UpdateUserSuccess()
        {
            var num = GenerateRandomNumber.Next(10000);
            var updateUser = await ursv_.UpdateUser(1, new UpdateUserOptions()
            {
                UserFirstName = "Nikitas",
                UserLastName = "Xanthos",
                UserPhone = "+306945082345",
                UserVat = "123456789",
                UserEmail = $"updated{num}@gmail.com"
            });

            Assert.Equal(Core.StatusCode.Ok, updateUser.ErrorCode);
        }

        [Fact]
        public async Task UpdateUserFailure_Invalid_Id()
        {
            var updateUser = await ursv_.UpdateUser(0, new UpdateUserOptions()
            {
                UserFirstName = "Nikitas",
                UserLastName = "Xanthos",
                UserPhone = "+306945082345",
                UserVat = "123456789",
                UserEmail = "nikitasxts@gmail.com"
            });

            Assert.Equal(Core.StatusCode.BadRequest, updateUser.ErrorCode);
        }

        [Fact]
        public async Task UpdateUserFailure_Null_Options()
        {
            var updateUser = await ursv_.UpdateUser(1, default(UpdateUserOptions));

            Assert.Equal(Core.StatusCode.BadRequest, updateUser.ErrorCode);
        }

        [Fact]
        public async Task UpdateUserFailure_Id_Not_Found()
        {
            var updateUser = await ursv_.UpdateUser(999999, new UpdateUserOptions()
            {
                UserFirstName = "Nikitas",
                UserLastName = "Xanthos",
                UserPhone = "+306945082345",
                UserVat = "123456789",
                UserEmail = "nikitasxts@gmail.com"
            });

            Assert.Equal(Core.StatusCode.NotFound, updateUser.ErrorCode);
        }

        [Fact]
        public async Task UpdateUserFailure_Email_Already_Exists()
        {
            var updateUser = await ursv_.UpdateUser(1, new UpdateUserOptions()
            {
                UserFirstName = "Nikitas",
                UserLastName = "Xanthos",
                UserPhone = "+306945082345",
                UserVat = "123456789",
                UserEmail = "nikitasxts@gmail.com"
            });

            Assert.Equal(Core.StatusCode.Conflict, updateUser.ErrorCode);
        }

        /// <summary>
        /// Search User Tests
        /// </summary>
        [Fact]
        public void SearchUserById()
        {
            var searchOptions = new SearchUserOptions()
            {
                UserId = 5
            };

            var result = ursv_.SearchUser(searchOptions);

            Assert.NotNull(result);
        }

        [Fact]
        public void SearchUserByEmail()
        {
            var searchOptions = new SearchUserOptions()
            {
                UserEmail = "8024495@gmail.com"
            };

            var result = ursv_.SearchUser(searchOptions);

            Assert.NotNull(result);
        }
    }
}
