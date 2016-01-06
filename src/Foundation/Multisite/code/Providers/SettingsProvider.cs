using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Web;

  public class SettingsProvider : ISettingsProvider
  {
    private readonly SiteContext siteContext;

    public SettingsProvider() : this(new SiteContext())
    { 
    }

    public SettingsProvider(SiteContext siteContext)
    {
      this.siteContext = siteContext;
    }

    public static string SettingsRootName => Sitecore.Configuration.Settings.GetSetting("Multisite.SettingsRootName", "settings");

    public virtual Item GetSettingItem(string settingName, Item contextItem)
    {

      var currentDefinition = this.siteContext.GetSiteDefinition(contextItem);
      if (currentDefinition == null)
      {
        return null;
      }

      var definitionItem = currentDefinition.Item;
      var settingsFolder = definitionItem.Children[SettingsRootName];

      var sourceSettingItem = settingsFolder?.Children.FirstOrDefault(x => x.IsDerived(Templates.SiteSettings.ID) && x.Key.Equals(settingName.ToLower()));

      return sourceSettingItem;
    }

    public virtual SiteInfo GetCurrentSiteInfo(Item contextItem)
    {
      var context = new SiteContext();

      var currentDefinition = context.GetSiteDefinition(contextItem);
      if (currentDefinition == null)
      {
        {
          return null;
        }
      }

      var siteInfo = Sitecore.Configuration.Factory.GetSiteInfo(currentDefinition.Name);
      return siteInfo;
    }
  }
}