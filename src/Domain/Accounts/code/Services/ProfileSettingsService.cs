namespace Sitecore.Feature.Accounts.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore;
  using Sitecore.Configuration;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.SecurityModel;

  public class ProfileSettingsService : IProfileSettingsService
  {
    public virtual Item GetUserDefaultProfile()
    {
      using (new SecurityDisabler())
      {
        var item = GetSettingsItem(Context.Item);
        Assert.IsNotNull(item, "Page with profile settings isn't specified");
        var database = Sitecore.Data.Database.GetDatabase(Settings.ProfileItemDatabase);
        var profileField = item.Fields[Templates.ProfileSettigs.Fields.UserProfile];
        var targetItem = database.GetItem(profileField.Value);
        
        return targetItem;
      }
    }

    public virtual IEnumerable<string> GetInterests()
    {
      var item = GetSettingsItem(null);
      ReferenceField interestsFolder = item.Fields[Templates.ProfileSettigs.Fields.InterestsFolder];

      return interestsFolder?.TargetItem?.GetChildrenDerivedFrom(Templates.Interest.ID)?.Select(i => i.Fields[Templates.Interest.Fields.Title].Value) ?? Enumerable.Empty<string>();
    }

    private static Item GetSettingsItem(Item contextItem)
    {
      Item item = null;

      if (contextItem != null)
      {
        item = contextItem.GetAncestorOrSelfOfTemplate(Templates.ProfileSettigs.ID);
      }
      item = item ?? Context.Site.GetContextItem(Templates.ProfileSettigs.ID);

      return item;
    }
  }
}