using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model.Options.Project
{
   public class SearchProjectOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string ProjectTitle { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public ProjectCategory ProjectCategory { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset ProjectDateExpiring { get; set; }

        /// <summary>
        /// 
        /// </summary>   
    }
}
