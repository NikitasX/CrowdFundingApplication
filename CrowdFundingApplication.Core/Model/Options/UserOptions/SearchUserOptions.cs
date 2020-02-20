using System;
using System.Collections.Generic;
using System.Text;

namespace CrowdFundingApplication.Core.Model.Options.User
{
    public class SearchUserOptions
    {
        public int? UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public string UserVat { get; set; }

        public DateTimeOffset? UserDateCreatedFrom { get; set; }

        public DateTimeOffset? UserDateCreatedTo { get; set; }

    }
}
