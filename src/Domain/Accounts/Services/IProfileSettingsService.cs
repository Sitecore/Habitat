using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Habitat.Accounts.Services
{
  public interface IProfileSettingsService
  {
    IEnumerable<string> GetInterests();
    Item GetUserDefaultProfile();
    IProfileProcessor GetUserProfileProcessor();
  }
}