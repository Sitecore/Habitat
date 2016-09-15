namespace Sitecore.Feature.Accounts.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.Data.Templates;
  using Sitecore.Diagnostics;
  using Sitecore.Security;
  using Sitecore.SecurityModel;

  public class UserProfileProvider : IUserProfileProvider
  {
    public IDictionary<string, string> GetCustomProperties(UserProfile userProfile)
    {
      Assert.ArgumentNotNull(userProfile, nameof(userProfile));

      var template = this.GetProfileTemplate(userProfile.ProfileItemId);

      return template?.GetFields(true).ToDictionary(k => k.Name, v => userProfile[v.Name]) ?? new Dictionary<string, string>();
    }

    public void SetCustomProfile(UserProfile userProfile, IDictionary<string, string> properties)
    {
      Assert.ArgumentNotNull(userProfile, nameof(userProfile));
      Assert.ArgumentNotNull(properties, nameof(properties));

      foreach (var property in properties)
      {
        if (property.Value == null)
        {
          userProfile[property.Key] = string.Empty;
        }
        else
        {
          userProfile[property.Key] = property.Value;
        }
      }

      userProfile.Save();
    }

    protected Template GetProfileTemplate(string profileItemId)
    {
      using (new SecurityDisabler())
      {
        ID profileId;

        if (!ID.TryParse(profileItemId, out profileId))
          return null;
        var database = Database.GetDatabase(Settings.ProfileItemDatabase);
        var item = database?.GetItem(profileId);

        return item != null ? TemplateManager.GetTemplate(item) : null;
      }
    }
  }
}