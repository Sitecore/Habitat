namespace Sitecore.Foundation.Multisite.Providers
{
  using System;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Multisite.Providers;

  public class ItemDatasourceProvider : IDatasourceProvider
  {
    private ISettingsProvider settingsProvider;

    public ItemDatasourceProvider() : this(new SettingsProvider())
    {
    }

    public ItemDatasourceProvider(ISettingsProvider settingsProvider)
    {
      this.settingsProvider = settingsProvider;
    }

    public Item[] GetDatasources(string settingName, Item contextItem)
    {
      var sources = new Item[]
      {
      };

      var sourceSettingItem = this.settingsProvider.GetSettingItem(settingName, contextItem);
      var datasourceRoot = sourceSettingItem?[Templates.DatasourceConfiguration.Fields.DatasourceLocation];

      if (!string.IsNullOrEmpty(datasourceRoot))
      {
        if (datasourceRoot.StartsWith("query:", StringComparison.InvariantCulture))
        {
           sources = this.AddRootsFromQuery(datasourceRoot.Substring("query:".Length), contextItem);
        }
        else if (datasourceRoot.StartsWith("./", StringComparison.InvariantCulture)) 
        {
          string path = datasourceRoot;
          path = contextItem.Paths.FullPath + datasourceRoot.Remove(0, 1);

          Item root = this.Database.GetItem(path);
          if (root != null)
          {
            sources = new [] { root };
          }
        }
        else
        {
          var sourceRootItem = this.Database.GetItem(datasourceRoot);
          if (sourceRootItem != null)
          {
            sources = new[] { sourceRootItem };
          }
        }
      }

      return sources;
    }

    protected virtual Item[] AddRootsFromQuery(string query, Item contextItem)
    {
      Item[] roots = (Item[])null;
      if (query.StartsWith("./", StringComparison.InvariantCulture) && contextItem != null)
      {
        roots = contextItem.Axes.SelectItems(query);
      }
      else
      {
        roots = this.Database.SelectItems(query);
      }

      return roots;
    }

    public Item GetDatasourceTemplate(string settingName, Item contextItem)
    {
      var settingItem = this.settingsProvider.GetSettingItem(settingName, contextItem);

      var templateId = settingItem?[Templates.DatasourceConfiguration.Fields.DatasourceTemplate];

      if (!string.IsNullOrEmpty(templateId))
      {
        var sourceRootItem = this.Database.GetItem(templateId);

        return sourceRootItem;
      }

      return null;
    }

    public Database Database { get; set; }
  }
}