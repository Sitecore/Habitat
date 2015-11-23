using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.Security;

namespace Habitat.Accounts.Services
{
  using Habitat.Accounts.Models;
  using Sitecore.Data.Items;

  public interface IUserProfileService
  {
    string GetUserDefaultProfileId();
    EditProfile GetEmptyProfile();
    EditProfile GetProfile(UserProfile userProfile);
    void SetProfile(UserProfile userProfile, EditProfile model);
    bool ValidateProfile(EditProfile model, ModelStateDictionary modelState);
    IEnumerable<string> GetInterests();
  }
}