using System;

namespace CrowdFundingApplication.Core.Model
{
    public class BackedIncentives
    {
        
        //CARE!! BACKER CANT BE THE SAME AS CREATOR
        
        public int UserId { get; set; }
        public User BackedUser { get; set; }

        public int IncentiveId { get; set; }
        public Incentive BackedIncentive { get; set; }

        public DateTimeOffset BackedIncentiveDateCreated { get; set; }

        public BackedIncentives()
        {
            BackedIncentiveDateCreated = DateTimeOffset.Now;
        }
    }
}
