using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model
{
    public class Incentive
    {
        /// <summary>
        /// 
        /// </summary>
        public int IncentiveId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IncentiveTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IncentiveDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal IncentivePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IncentiveReward { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset IncentiveDateCreated { get; set; }        
        
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BackedIncentives> IncentiveBackers { get; set; }

        public Incentive()
        {
            IncentiveBackers = new List<BackedIncentives>();
            IncentiveDateCreated = DateTimeOffset.Now;
        }
    }
}
