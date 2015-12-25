using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class ItemDatasourceProvider : IDatasourceProvider
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

      var sourceSettingItem = this.GetSettingItem(settingName, contextItem);

      var datasourceRoot = sourceSettingItem?[Templates.DatasourceConfiguration.Fields.DatasourceLocation];

      if (!string.IsNullOrEmpty(datasourceRoot))
      {
        var sourceRootItem = database.GetItem(datasourceRoot);
        if (sourceRootItem != null)
        {
          sources = new[] { sourceRootItem };
        }
      }

      if (!sources.Any())
      {
        var fallbackProvider = new ConfigurationDataSourceProvider(database);
        sources = fallbackProvider.GetSources(settingName, contextItem);
      }

      return sources;
    }

    protected virtual Item GetSettingItem(string settingName, Item contextItem)
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


    public Item GetSourceTemplate(string settingName, Item contextItem)
    {
      var settingItem = this.GetSettingItem(settingName, contextItem);

      var templateId = settingItem?[Templates.DatasourceConfiguration.Fields.DatasourceTemplate];

      if (!string.IsNullOrEmpty(templateId))
      {
        var sourceRootItem = database.GetItem(templateId);

        return sourceRootItem;
      }

      var fallbackProvider = new ConfigurationDataSourceProvider(database);
      return fallbackProvider.GetSourceTemplate(settingName, contextItem);
    }
  }
}