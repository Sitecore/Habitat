using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.Security;

namespace Habitat.Accounts.Services
{
  using Sitecore.Data.Items;

  public interface IUserProfileService
  {
    IDictionary<string, string> GetProfile(UserProfile userProfile);
    void SetProfile(UserProfile userProfile, IDictionary<string, string> properties);
    ModelStateDictionary Validate(IDictionary<string, string> properties);
    Item GetUserDefaultProfile();
    IEnumerable<string> GetInterests();
  }
}