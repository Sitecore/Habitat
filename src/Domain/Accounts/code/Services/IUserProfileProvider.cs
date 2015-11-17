namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using Sitecore.Security;

  public interface IUserProfileProvider
  {
    IDictionary<string, string> GetCustomProperties(UserProfile userProfile);
    void SetCustomProfile(UserProfile userProfile, IDictionary<string,string> properties);
  }
}