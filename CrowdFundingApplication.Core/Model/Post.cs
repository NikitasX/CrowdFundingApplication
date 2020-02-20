using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model
{
   public class Post
   {   
        /// <summary>
        /// 
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User PostUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Project PostProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostExcerpt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset PostDateCreated { get; set; }
    }
}
