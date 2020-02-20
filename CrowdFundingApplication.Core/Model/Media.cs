using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model
{
    public class Media
    {
        /// <summary>
        /// 
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Project Project { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public MediaTypes MediaType { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public string MediaURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset MediaDateCreated { get; set; }

        public Media()
        {
            MediaDateCreated = DateTimeOffset.Now;
        }
    }
}
