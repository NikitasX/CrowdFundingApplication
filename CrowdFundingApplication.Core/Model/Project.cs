using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model
{
   public class Project
   {   
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>       
        public User User { get; set; }

        /// <summary>
        /// 
        /// </summary>      
        public string ProjectTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>  
        public decimal ProjectFinancialGoal { get; set; }

        /// <summary>
        /// 
        /// </summary>     
        public decimal ProjectCapitalAcquired { get; set; }

        /// <summary>
        /// 
        /// </summary>    
        public ProjectCategory ProjectCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset ProjectDateCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset ProjectDateExpiring { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Post> ProjectPosts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Incentive> ProjectIncentives { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
