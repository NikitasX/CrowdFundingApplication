using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdFundingApplication.Web.Models
{
    public class AddProjectViewModel
    {
        public CrowdFundingApplication.Core.Model.Options.ProjectOptions.AddProjectOptions AddOptions { get; set; }
        public string ErrorText { get; set; }
    }
}
