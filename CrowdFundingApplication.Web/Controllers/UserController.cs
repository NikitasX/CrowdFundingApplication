using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingApplication.Web.Controllers
{
    public class UserController : Controller
    {
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
    }
}
