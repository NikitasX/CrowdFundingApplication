using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model.Options.IncentiveOptions
{
    public class SearchIncentiveOptions
    {
        public int IncentiveId { get; set; }

        public string IncentiveTitle { get; set; }

        public decimal IncentivePriceFrom { get; set; }

        public decimal IncentivePriceTo { get; set; }

        public DateTimeOffset IncentiveDateCreatedFrom { get; set; }

        public DateTimeOffset IncentiveDateCreatedTo { get; set; }

    }
}
