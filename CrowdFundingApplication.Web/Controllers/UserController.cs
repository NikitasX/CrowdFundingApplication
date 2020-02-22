using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CrowdFundingApplication.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly CrowdFundingDbContext context_;
        private readonly IUserService users_;

        public UserController(
            CrowdFundingDbContext ctx,
            IUserService usr)
        {
            context_ = ctx;
            users_ = usr;
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
    }
}
