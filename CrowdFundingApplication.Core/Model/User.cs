using System;

namespace CrowdFundingApplication.Core.Model
{
    public class User
    {
        public int UserId { get; set; }
        
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public string UserVat { get; set; }

        public DateTimeOffset UserDateCreated { get; set; }

        public User()
        {
            UserDateCreated = DateTimeOffset.Now;
        }
    }
}
