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
    public virtual Item GetSettingItem(string settingName, Item contextItem)
    {
      var siteContext = new SiteContext();

      var currentDefinition = siteContext.GetSiteDefinitionByItem(contextItem);
      if (currentDefinition == null)
      {
        return null;
      }

      var definitionItem = currentDefinition.Item;
      var settingsFolder = definitionItem.Children["settings"];

      var sourceSettingItem = settingsFolder?.Children.FirstOrDefault(x => x.IsDerived(Templates.DatasourceConfiguration.ID) && x.Key.Equals(settingName));

      return sourceSettingItem;
    }

    public virtual SiteInfo GetCurrentSiteInfo(Item contextItem)
    {
      var siteContext = new SiteContext();

      var currentDefinition = siteContext.GetSiteDefinitionByItem(contextItem);
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