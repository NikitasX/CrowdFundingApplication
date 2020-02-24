using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Web.Extensions;
using CrowdFundingApplication.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CrowdFundingApplication.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly CrowdFundingDbContext context_;
        private readonly IUserService users_;
        private readonly IIncentiveService incentive_;

        public UserController(
            CrowdFundingDbContext ctx,
            IUserService usr,
            IIncentiveService inc)
        {
            context_ = ctx;
            users_ = usr;
            incentive_ = inc;
        }
        public IActionResult AddUser()
        {
            return View();
        }
        public IActionResult UpdateUser()
        {
            return View();
        }
        public IActionResult SearchUser()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> AddUser(

          [FromBody] Core.Model.Options.User.AddUserOptions options) 

        {
            var result = await users_.AddUser(
                options);
            return result.AsStatusResult();

        }

        public IActionResult ListUsers()
        {
            return Json(context_
                .Set<User>()
                .Include(u =>u.UserId )
                .Take(100)
                .AsQueryable(),
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
        [HttpGet("user/view/{id}")]
        public async Task<IActionResult> UserDashBoard(int id)
        {
            var user = await users_.GetUserById(id);


            var model = new UserDashboardViewModel()
            {
                User = user,
                Context = context_
            };

            if (model.User.ErrorCode == Core.StatusCode.Ok) {
                return View(model);
            } else {
                return model.User.AsStatusResult();
            }
        }
        //[HttpGet("user/getbyid/{userId}/{projectId}")]
        //public async Task<IActionResult> GetUserById(int projectId, int userId)
        //{
        //    var user = await users_.GetUserById(userId);
        //    var created = context_.Set<Project>().Where(u => u.User == user.Data);

        //    var backed = await incentive_.GetIncentiveByProjectId(projectId);

        //    return Json(backed.Data,
        //        new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });
        //}
      
        }
    }

