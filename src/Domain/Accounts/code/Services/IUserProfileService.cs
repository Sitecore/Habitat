using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.Security;

namespace Habitat.Accounts.Services
{
  using Sitecore.Data.Items;

  public interface IUserProfileService
  {
    Item GetUserDefaultProfile();
    object GetProfile(UserProfile userProfile);
    void SetProfile(UserProfile userProfile, object profileModel);
    object GetEmptyProfile();
    bool ValidateProfile(object profileModel, ModelStateDictionary modelState);
  }
}