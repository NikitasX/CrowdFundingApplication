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
        public User ProjectUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset ProjectDateExpiring { get; set; }

        /// <summary>
        /// 
        /// </summary>      
        public string ProjectTitle { get; set; }

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
    }
}
