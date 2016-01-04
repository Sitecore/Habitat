﻿using System;
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
    private ISettingsProvider settingsProvider;

    public ItemDatasourceProvider() : this(new SettingsProvider())
    {
    }

    public ItemDatasourceProvider(ISettingsProvider settingsProvider)
    {
      this.settingsProvider = settingsProvider;
    }

    public Item[] GetSources(string settingName, Item contextItem)
    {
      var sources = new Item[]
      {
      };

      var sourceSettingItem = this.settingsProvider.GetSettingItem(settingName, contextItem);

      var datasourceRoot = sourceSettingItem?[Templates.DatasourceConfiguration.Fields.DatasourceLocation];

      if (!string.IsNullOrEmpty(datasourceRoot))
      {
        var sourceRootItem = this.Database.GetItem(datasourceRoot);
        if (sourceRootItem != null)
        {
          sources = new[] { sourceRootItem };
        }
      }

      return sources;
    }

    public Item GetSourceTemplate(string settingName, Item contextItem)
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