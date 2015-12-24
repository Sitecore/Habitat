using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class ItemDatasourceProvider :IDatasourceProvider
  {
    private readonly Database database;

    public ItemDatasourceProvider(Database database)
    {
      this.database = database;
    }
    public Item[] GetSources(string settingName, Item contextItem)
    {
      var sources = new Item[]
      {
      };

      var definitions = new ItemSiteDefinitionsProvider();
      var siteContext = new SiteContext();
      
      var currentDefinition = siteContext.GetSiteDefinitionByItem(contextItem);
      if (currentDefinition == null)
      {
        return sources;
      }
      var definitionItem = currentDefinition.Item;
      var settingsFolder = definitionItem.Children["settings"];

      if (settingsFolder == null)
      {
        return sources;
      }

      var sourceSettingItem = settingsFolder.Children.FirstOrDefault(x => x.IsDerived(Templates.DatasourceConfiguration.ID) && x.Key.Equals(settingName));

      if (sourceSettingItem == null)
      {
        return sources;
      }

      var datasourceRoot = sourceSettingItem[Templates.DatasourceConfiguration.Fields.DatasourceLocation];

      if (!string.IsNullOrEmpty(datasourceRoot))
      {
        var sourceRootItem = database.GetItem(datasourceRoot);
        if (sourceRootItem == null)
        {
          var fallbackProvider = new ConfigurationDataSourceProvider();

          sources = fallbackProvider.GetSources(settingName, contextItem);
        }
        else
        {
          sources = new[] {sourceRootItem};
        }
      }

      return sources;
    }
  }
}