using System;

namespace CrowdFundingApplication.Core.Model.Options.Project
{
    public class SearchProjectOptions
    {
        public int ProjectId { get; set; }

        public string ProjectTitle { get; set; }

        public ProjectCategory ProjectCategory { get; set; }
        
        public DateTimeOffset ProjectDateExpiring { get; set; }
    }
}
