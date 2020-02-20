using System;

namespace CrowdFundingApplication.Core.Model.Options.ProjectOptions
{
    public class AddProjectOptions
    {
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
