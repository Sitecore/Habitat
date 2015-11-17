namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Reflection;

  public class ProfileSettingsService : IProfileSettingsService
  {
    public virtual Item GetUserDefaultProfile()
    {
      var item = GetSettingsItem(Context.Item);
      Assert.IsNotNull(item, "Page with accounts settings isn't specified");
      ReferenceField profileField = item.Fields[Templates.ProfileSettigs.Fields.UserProfile];
      Assert.IsNotNull(profileField.TargetItem, "Default user profile isn't specified");

      return profileField.TargetItem;
    }

    public virtual IProfileProcessor GetUserProfileProcessor()
    {
      var item = GetSettingsItem(Context.Item);
      Assert.IsNotNull(item, "Page with accounts settings isn't specified");
      var processorField = item.Fields[Templates.ProfileSettigs.Fields.UserProfileProcessor];
      Assert.IsNotNullOrEmpty(processorField.Value, "Default user profile isn't specified");

      var processor = ReflectionUtil.CreateObject(processorField.Value) as IProfileProcessor;
      Assert.IsNotNull(processor, $"Can't create instance of user profile with type name: {processorField.Value}");

      return processor;
    }

    public virtual IEnumerable<string> GetInterests()
    {
      var item = GetSettingsItem(null);
      ReferenceField interestsFolder = item.Fields[Templates.ProfileSettigs.Fields.InterestsFolder];

      return interestsFolder.TargetItem.GetChildrenDerivedFrom(Templates.Interest.ID).Select(i => i.Fields[Templates.Interest.Fields.Title].Value);
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