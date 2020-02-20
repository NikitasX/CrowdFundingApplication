using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model
{
    public class BackedIncentives
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        public User BackedUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IncentiveId { get; set; }
        public Incentive BackedIncentive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset BackedIncentiveDateCreated { get; set; }

        public BackedIncentives()
        {
            BackedIncentiveDateCreated = DateTimeOffset.Now;
        }
    }
}
