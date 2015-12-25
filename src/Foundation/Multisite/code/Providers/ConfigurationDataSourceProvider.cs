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
    private Database database;
    private const string locationPostfix = "sourceLocation";
    private const string sourceTemplatePostfix = "sourceTemplate";

    public ConfigurationDataSourceProvider(Database database)
    {
      this.database = database;
    }

    public Item[] GetSources(string name, Item contextItem)
    {
      var sources = new Item[] {};

      var siteInfo = this.GetCurrentSiteInfo(contextItem);

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

      var dataSourceLocation = database.GetItem(sourceLocation);
      if (dataSourceLocation == null)
      {
        return sources;
      }

      sources = new [] {dataSourceLocation};

      return sources;
    }

    protected virtual  SiteInfo GetCurrentSiteInfo(Item contextItem)
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

    public Item GetSourceTemplate(string settingName, Item contextItem)
    {
      var siteInfo = this.GetCurrentSiteInfo(contextItem);

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

      var dataSourceLocation = database.GetItem(sourceTemplate);

      return dataSourceLocation;

    }
  }
}