using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdFundingApplication.Core;
using CrowdFundingApplication.Core.Data;
using CrowdFundingApplication.Core.Model;

namespace CrowdFundingApplication.Web.Models
{
    public class UserDashboardViewModel
    {
        public ApiResult<User> User { get; set; }

        public CrowdFundingDbContext Context { get; set; }
    }
}
