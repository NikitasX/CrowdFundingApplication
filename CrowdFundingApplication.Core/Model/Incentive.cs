using System;
using System.Collections.Generic;

namespace CrowdFundingApplication.Core.Model
{
    public class Incentive
    {
        public int IncentiveId { get; set; }

        public Project Project { get; set; }

        public string IncentiveTitle { get; set; }

        public string IncentiveDescription { get; set; }

        public decimal IncentivePrice { get; set; }

        public string IncentiveReward { get; set; }

        public DateTimeOffset IncentiveDateCreated { get; set; }        
        
        public ICollection<BackedIncentives> IncentiveBackers { get; set; }

        public Incentive()
        {
            IncentiveBackers = new List<BackedIncentives>();
            IncentiveDateCreated = DateTimeOffset.Now;
        }
    }
}
