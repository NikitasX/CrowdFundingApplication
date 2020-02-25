namespace CrowdFundingApplication.Web.Models
{
    public class AddUserViewModel
    {
        public CrowdFundingApplication.Core.Model.Options.User.AddUserOptions AddOptions { get; set; }
        public string ErrorText { get; set; }
    }
}
