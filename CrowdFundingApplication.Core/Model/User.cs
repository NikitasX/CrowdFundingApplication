using System;
using System.Collections.Generic;

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

        public ICollection<Project> UserCreatedProjects { get; set; }

        public DateTimeOffset UserDateCreated { get; set; }

        public User()
        {
            UserCreatedProjects = new List<Project>();
            UserDateCreated = DateTimeOffset.Now;
        }
    }
}
