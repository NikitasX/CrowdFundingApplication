using System;

namespace CrowdFundingApplication.Core.Model.Options.Project
{
    public  class UpdateProjectOptions
    {   
        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public decimal ProjectFinancialGoal { get; set; }
    
        public decimal ProjectCapitalAcquired { get; set; }
  
        public ProjectCategory ProjectCategory { get; set; }

        public DateTimeOffset ProjectDateExpiring { get; set; }
    }
}
