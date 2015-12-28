using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Web;

  public class ConfigurationDataSourceProvider : IDatasourceProvider
  {

    private const string locationPostfix = "sourceLocation";
    private const string sourceTemplatePostfix = "sourceTemplate";
    private ISettingsProvider settingsProvider;

    public ConfigurationDataSourceProvider() : this(new SettingsProvider())
    {
    }

    public ConfigurationDataSourceProvider(ISettingsProvider settingsProvider)
    {
      this.settingsProvider = settingsProvider;
    }

    public virtual Item[] GetSources(string name, Item contextItem)
    {
      var sources = new Item[] {};

      var siteInfo = this.settingsProvider.GetCurrentSiteInfo(contextItem);

      if (siteInfo == null)
      {
        return sources;
      }

      var sourceLocationPropertyName = $"{name}.{locationPostfix}";
      var sourceLocation = siteInfo.Properties[sourceLocationPropertyName];

      if (string.IsNullOrEmpty(sourceLocation))
      {
        return sources;
      }

      var dataSourceLocation = this.Database.GetItem(sourceLocation);
      if (dataSourceLocation == null)
      {
        return sources;
      }

      sources = new [] {dataSourceLocation};

      return sources;
    }

    public virtual Item GetSourceTemplate(string settingName, Item contextItem)
    {
      var siteInfo = this.settingsProvider.GetCurrentSiteInfo(contextItem);

      if (siteInfo == null)
      {
        return null;
      }

      var sourceTemplatePropertyName = $"{settingName}.{sourceTemplatePostfix}";
      var sourceTemplate = siteInfo.Properties[sourceTemplatePropertyName];

      if (string.IsNullOrEmpty(sourceTemplate))
      {
        return null;
      }

      var dataSourceLocation = this.Database.GetItem(sourceTemplate);

      return dataSourceLocation;

    }

    public Database Database { get; set; }
  }
}