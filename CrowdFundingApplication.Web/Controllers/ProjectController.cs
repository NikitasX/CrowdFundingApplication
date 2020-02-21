using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrowdFundingApplication.Web.Models;

namespace CrowdFundingApplication.Web.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult AddProject()
        {
            return View();
        }
        public IActionResult ProjectList()
        {
            return View();
        }
    }
}
