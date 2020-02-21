using System;

namespace CrowdFundingApplication.Core.Model.Options.ProjectOptions
{
    public class AddProjectOptions
    {   
        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public decimal ProjectFinancialGoal { get; set; }

        public ProjectCategory ProjectCategory { get; set; }

        public DateTimeOffset ProjectDateExpiring { get; set; }
    }
}
