namespace Sitecore.Feature.Accounts.Services
{
    using System.Collections.Generic;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Security;
    using Sitecore.Security.Accounts;

    public interface IUserProfileService
    {
        string GetUserDefaultProfileId();
        EditProfile GetEmptyProfile();
        EditProfile GetProfile(User user);
        void SaveProfile(UserProfile userProfile, EditProfile model);
        IEnumerable<string> GetInterests();
    }
}