﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;
using CrowdFundingApplication.Core.Model.Options.IncentiveOptions;
using CrowdFundingApplication.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrowdFundingApplication.Core.Services
{
    public class IncentiveService : IIncentiveService
    {

        private readonly CrowdFundingDbContext context;
        private readonly IProjectService projects;
        private readonly ILoggerService logger;

        public IncentiveService(
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

        public async Task<ApiResult<Incentive>> AddIncentive
            (int projectId, AddIncentiveOptions options)
        {
            if(projectId <= 0) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    "Project id cannot be equal to or less than 0");
            }

            if(options == null) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    $"Incentive options: \'{nameof(options)}\' cannot be null");
            }

            if(string.IsNullOrWhiteSpace(options.IncentiveTitle)) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    $"Incentive title cannot be null or whitespace");
            }            
            
            if(options.IncentivePrice <= 0) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    $"Incentive price cannot be equal to or less than 0");
            }            
            
            if(string.IsNullOrWhiteSpace(options.IncentiveReward)) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    $"Incentive title cannot be null or whitespace");
            }

            var project = await projects.GetProjectById(projectId);

            if(!project.Success) {
                return project.ToResult<Incentive>();
            }

            var incentive = new Incentive()
            {
                Project = project.Data,
                IncentiveTitle = options.IncentiveTitle,
                IncentiveDescription = options.IncentiveDescription,
                IncentivePrice = options.IncentivePrice,
                IncentiveReward = options.IncentiveReward
            };

            context.Add(incentive);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Changes not saved, incentive not added.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Incentive>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, incentive not added");
            }

            if (success) {
                return ApiResult<Incentive>.CreateSuccess(incentive);
            } else {
                return new ApiResult<Incentive>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, incentive not added");
            }
        }

        public async Task<ApiResult<Incentive>> UpdateIncentive
            (int incentiveId, UpdateIncentiveOptions options)
        {
            if (incentiveId <= 0) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    "Incentive id cannot be equal to or less than 0");
            }

            if (options == null) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    $"Incentive options: \'{nameof(options)}\' cannot be null");
            }

            var incentive = await SearchIncentive(new SearchIncentiveOptions()
            {
                IncentiveId = incentiveId
            }).SingleOrDefaultAsync();

            if(incentive == null) {
                return new ApiResult<Incentive>(
                    StatusCode.NotFound,
                    $"Incentive Id not found in database");
            }

            if(!string.IsNullOrWhiteSpace(options.IncentiveTitle)) {
                incentive.IncentiveTitle = options.IncentiveTitle;
            }

            if(!string.IsNullOrWhiteSpace(options.IncentiveDescription)) {
                incentive.IncentiveDescription = options.IncentiveDescription;
            }            
            
            if(options.IncentivePrice > 0) {
                incentive.IncentivePrice = options.IncentivePrice;
            }            
            
            if(!string.IsNullOrWhiteSpace(options.IncentiveReward)) {
                incentive.IncentiveReward = options.IncentiveReward;
            }

            context.Update(incentive);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Changes not saved, incentive not updated.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Incentive>(
                    StatusCode.InternalServerError,
                    $"Changes not saved, incentive not updated");
            }

            if (success) {
                return ApiResult<Incentive>.CreateSuccess(incentive);
            } else {
                return new ApiResult<Incentive>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, incentive not updated");
            }
        }

        public async Task<ApiResult<Incentive>> RemoveIncentive
            (int incentiveId)
        {
            if (incentiveId <= 0) {
                return new ApiResult<Incentive>(
                    StatusCode.BadRequest,
                    "Incentive id cannot be equal to or less than 0");
            }

            var incentive = await SearchIncentive(new SearchIncentiveOptions()
            {
                IncentiveId = incentiveId
            }).SingleOrDefaultAsync();

            if (incentive == null) {
                return new ApiResult<Incentive>(
                    StatusCode.NotFound,
                    "Incentive id not found in database");
            }

            context.Remove(incentive);

            var success = false;

            try {
                success = await context.SaveChangesAsync() > 0;
            } catch (Exception e) {

                logger.LogError(StatusCode.InternalServerError.ToString(),
                        $"Incentive not deleted.");
                logger.LogInformation(e.ToString());

                return new ApiResult<Incentive>(
                    StatusCode.InternalServerError,
                    $"Incentive not deleted");
            }

            ///DIMITRIS CHECK
            if (success) {
                return ApiResult<Incentive>.CreateSuccess(incentive);
            } else {
                return new ApiResult<Incentive>(
                    StatusCode.InternalServerError,
                    $"Something went wrong, incentive not deleted");
            }
        }

        public async Task<ApiResult<IQueryable<Incentive>>> GetIncentiveByProjectId
            (int projectId)
        {
            if (projectId <= 0) {
                return new ApiResult<IQueryable<Incentive>>(
                    StatusCode.BadRequest,
                    "Project id cannot be equal to or less than 0");
            }

            var project = await projects.GetProjectById(projectId);

            if (!project.Success) {
                return project.ToResult<IQueryable<Incentive>>();
            }

            return ApiResult<IQueryable<Incentive>>.CreateSuccess(context
                .Set<Incentive>()
                .AsQueryable()
                .Where(m => m.Project == project.Data)
                .Take(500));
        }

        public IQueryable<Incentive> SearchIncentive
            (SearchIncentiveOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context
                .Set<Incentive>()
                .AsQueryable();

            if (options.IncentiveId != 0) {
                query = query
                    .Where(c => c.IncentiveId == options.IncentiveId);
            }            
            
            if (!string.IsNullOrWhiteSpace(options.IncentiveTitle)) {
                query = query
                    .Where(c => c.IncentiveTitle.Contains(options.IncentiveTitle));
            }

            if (options.IncentivePriceFrom > 0) {
                query = query
                    .Where(c => c.IncentivePrice > options.IncentivePriceFrom);
            }              
            
            if (options.IncentivePriceTo > 0) {
                query = query
                    .Where(c => c.IncentivePrice < options.IncentivePriceTo);
            }                
            
            if (options.IncentiveDateCreatedFrom != default(DateTimeOffset)) {
                query = query
                    .Where(c => c.IncentiveDateCreated > options.IncentiveDateCreatedFrom);
            }            
            
            if (options.IncentiveDateCreatedTo != default(DateTimeOffset)) {
                query = query
                    .Where(c => c.IncentiveDateCreated < options.IncentiveDateCreatedTo);
            }

            return query.Take(500);
        }

    }
}
