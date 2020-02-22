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
        
        [HttpGet("project/view/{id}")]
        public async Task<IActionResult> SingleProject(int id)
        {
            var model = await projects_.GetProjectById(id);

            return model.AsStatusResult();
        }

        [HttpGet("project/getbyid/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await projects_.GetProjectById(id);

            return Json(project.Data,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
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
    }
}
