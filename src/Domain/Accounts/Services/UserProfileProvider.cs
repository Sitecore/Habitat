namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.Data.Templates;
  using Sitecore.Diagnostics;
  using Sitecore.Security;

  public class UserProfileProvider : IUserProfileProvider
  {
    public IDictionary<string, string> GetCustomProperties(UserProfile userProfile)
    {
      Assert.ArgumentNotNull(userProfile, "userProfile");

      var template = this.GetProfileTemplate(userProfile.ProfileItemId);

      if (template != null)
      {
        return template.GetFields(false).ToDictionary(k => k.Name, v => userProfile[v.Name]);
      }

      return new Dictionary<string, string>();
    }

    public void SetCustomProfile(UserProfile userProfile, IDictionary<string, string> properties)
    {
      Assert.ArgumentNotNull(userProfile, "userProfile");
      Assert.ArgumentNotNull(properties, "properties");

      foreach (var property in properties)
      {
        userProfile[property.Key] = property.Value;
      }

      userProfile.Save();
    }

    protected Template GetProfileTemplate(string profileItemId)
    {
      ID profileId;
      if (ID.TryParse(profileItemId, out profileId))
      {
        var database = Database.GetDatabase(Settings.ProfileItemDatabase);
        var item = database?.GetItem(profileId);

        if (item != null)
        {
          return TemplateManager.GetTemplate(item);
        }
      }

      return null;
    }
  }
}