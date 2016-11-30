namespace Sitecore.Feature.Accounts.Services
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Security;
    using Sitecore.Security.Accounts;

    public interface IUserProfileService
    {
        string GetUserDefaultProfileId();
        EditProfile GetEmptyProfile();
        EditProfile GetProfile(UserProfile userProfile);
        void SetProfile(UserProfile userProfile, EditProfile model);
        bool ValidateProfile(EditProfile model, ModelStateDictionary modelState);
        IEnumerable<string> GetInterests();
        bool ValidateUser(User user);
    }
}