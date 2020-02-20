using System;
using System.Collections.Generic;

namespace CrowdFundingApplication.Core.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
    
        public User User { get; set; }
  
        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public decimal ProjectFinancialGoal { get; set; }
  
        public decimal ProjectCapitalAcquired { get; set; }
  
        public ProjectCategory ProjectCategory { get; set; }

        public DateTimeOffset ProjectDateCreated { get; set; }

        public DateTimeOffset ProjectDateExpiring { get; set; }

        public ICollection<Post> ProjectPosts { get; set; }

        public ICollection<Incentive> ProjectIncentives { get; set; }

        public ICollection<Media> ProjectMedia { get; set; }

        public Project()
        {
            ProjectCapitalAcquired = 0;
            ProjectPosts = new List<Post>();
            ProjectIncentives = new List<Incentive>();
            ProjectMedia = new List<Media>();
            ProjectDateCreated = DateTimeOffset.Now;
        }
    }
}
