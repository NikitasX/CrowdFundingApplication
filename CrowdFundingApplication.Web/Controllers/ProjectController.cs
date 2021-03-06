﻿using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Web.Extensions;
using CrowdFundingApplication.Core.Services.Interfaces;
using CrowdFundingApplication.Core.Model.Options.Project;
using CrowdFundingApplication.Core.Model.Options.PostOptions;

namespace CrowdFundingApplication.Web.Controllers
{
    public class ProjectController : Controller
    {

        private readonly CrowdFundingDbContext context_;
        private readonly IProjectService projects_;
        private readonly IMediaService media_;
        private readonly IIncentiveService incentives_;
        private readonly IPostService posts_;

        public ProjectController(
            CrowdFundingDbContext ctx,
            IPostService pst,
            IMediaService med,
            IIncentiveService inc,
            IProjectService prj)
        {
            context_ = ctx;
            projects_ = prj;
            posts_ = pst;
            incentives_ = inc;
            media_ = med;
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
        
        public IActionResult Result()
        {
            return View();
        }

        public IActionResult ListPopular()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search
            (SearchProjectOptions options)
        {
            var projectsList = await projects_
                .SearchProject(options)
                .ToListAsync();

            return View(projectsList);
        }

        [HttpPost("project/addprojectpost/{id}")]
        public async Task<IActionResult> AddProjectPost
            (int id, [FromBody] AddPostOptions options)
        {
            var result = await posts_.AddPost(id, 1, options);

            return result.AsStatusResult();
        }        
        
        [HttpPost("project/AddProjectBacker/{projectId}/{incentiveId}")]
        public async Task<IActionResult> AddProjectBacker
            (int projectId,  int incentiveId)
        {
            var result = await incentives_.AddIncentiveBacker(projectId, incentiveId, 1);

            return result.AsStatusResult();
        }
        
        [HttpGet("project/view/{id}")]
        public async Task<IActionResult> SingleProject(int id)
        {
            var model = await projects_.GetProjectById(id);

            if(model.ErrorCode == Core.StatusCode.Ok) {
                return View(model.Data);
            } else {
                return model.AsStatusResult();
            }
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
        
        public IActionResult ListProjectsPopular()
        {
            return Json(context_
                .Set<Project>()
                .Include(m => m.ProjectMedia)
                .Take(100)
                .OrderByDescending(p => p.ProjectCapitalAcquired)
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
            var result = await projects_.AddProject(1, options);

            return result.AsStatusResult();
        }       
        
        [HttpPost("project/AddProjectMedia/{projectId}")]
        public async Task<IActionResult> AddProjectMedia(int projectId,
           [FromBody] Core.Model.Options.MediaOptions.AddMediaOptions options)
        {
            var result = await media_.AddMedia(projectId, options);

            return result.AsStatusResult();
        }        
        
        [HttpPost("project/AddProjectIncentive/{projectId}")]
        public async Task<IActionResult> AddProjectIncentive(int projectId,
           [FromBody] Core.Model.Options.IncentiveOptions.AddIncentiveOptions options)
        {
            var result = await incentives_.AddIncentive(projectId, options);

            return result.AsStatusResult();
        }
    }
}
