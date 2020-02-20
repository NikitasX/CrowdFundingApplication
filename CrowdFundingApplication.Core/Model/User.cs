using System;
using System.Collections.Generic;

namespace CrowdFundingApplication.Core.Model
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string UserFirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserLastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserVat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Project> UserCreatedProjects { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset UserDateCreated { get; set; }

        public User()
        {
            UserCreatedProjects = new List<Project>();
            UserDateCreated = DateTimeOffset.Now;
        }
    }
}
