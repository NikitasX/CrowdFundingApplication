using System.Linq;
using Newtonsoft;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CrowdFundingApplication.Web.Extensions;

namespace CrowdFundingApplication.Web.Controllers
{
    public class ProjectController : Controller
    {

        private readonly CrowdFundingDbContext context_;
        private readonly IProjectService projects_;

        public ProjectController(
            CrowdFundingDbContext ctx,
            IProjectService prj)
        {
            context_ = ctx;
            projects_ = prj;
        }

        public IActionResult Index()
        {
            return View();
        }        
        
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult ListProjects()
        {
            return Json(context_
                .Set<Project>()
                .Include(m => m.ProjectMedia)
                .Take(100)
                .AsQueryable(), 
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(
           [FromBody] Core.Model.Options.ProjectOptions.AddProjectOptions options)
        {
            var result = await projects_.AddProject(1,
                options);


            return result.AsStatusResult();

        }
    }
}
