using System;

namespace CrowdFundingApplication.Core.Model
{
    public class Media
    {
        public int MediaId { get; set; }

        public Project Project { get; set; }
        
        public MediaTypes MediaType { get; set; } 

        public string MediaURL { get; set; }

        public DateTimeOffset MediaDateCreated { get; set; }

        public Media()
        {
            MediaDateCreated = DateTimeOffset.Now;
        }
    }
}
