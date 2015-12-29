using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Pipelines
{
  using System.Text.RegularExpressions;
  using Sitecore.Collections;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Pipelines.GetLookupSourceItems;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class SiteDataSource
  {
    private const string DatasourceLocationFieldName = "Datasource Location";
    public const string SiteSourceMatchPattern = "^\\$site\\[\\w*\\]$";
    private DatasourceProviderFactory providerFactory;

    public SiteDataSource():this(new DatasourceProviderFactory())
    {
    }

    public SiteDataSource(DatasourceProviderFactory factory)
    {
      this.providerFactory = factory;
    }

    public void Process(GetRenderingDatasourceArgs args)
    {
      var source = args.RenderingItem[DatasourceLocationFieldName];
      Assert.ArgumentNotNull((object)args, "args");
      if (!source.StartsWith("$site"))
      {
        return;
      }

      this.ResolveSources(args);
      this.ResolveTemplate(args);
    }

    protected virtual void ResolveSources(GetRenderingDatasourceArgs args)
    {
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var source = args.RenderingItem["Datasource Location"];
      var name = this.GetSourceSettingName(source);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      var sources = new Item[] {};
      var provider = this.providerFactory.GetProvider(args.ContentDatabase);
      if (provider != null)
      {
        sources = provider.GetSources(name, contextItem);
      }


      

      if (!sources.Any())
      {
        provider = providerFactory.GetFallbackProvider(args.ContentDatabase);
        if (provider == null)
        {
          return;
        }

        sources = provider.GetSources(name, contextItem);
      }


      args.DatasourceRoots.AddRange(sources);
    }

    protected virtual void ResolveTemplate(GetRenderingDatasourceArgs args)
    {
      
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var source = args.RenderingItem["Datasource Location"];
      var name = this.GetSourceSettingName(source);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      Item sourceTemplate = null;

      var provider = this.providerFactory.GetProvider(args.ContentDatabase);
      if (provider != null)
      {
        sourceTemplate = provider.GetSourceTemplate(name, contextItem);
      }

      if (sourceTemplate == null)
      {
        provider = this.providerFactory.GetFallbackProvider(args.ContentDatabase);
        if (provider != null)
        {
          sourceTemplate = provider.GetSourceTemplate(name, contextItem);
        }
      }

      args.Prototype =  sourceTemplate;
    }

    public string GetSourceSettingName(string source)
    {
      var match = Regex.Match(source, SiteSourceMatchPattern);
      if (match.Success)
      {
        var value = match.Value;
        var settingNameMatch = Regex.Match(value, "\\[\\w*\\]");
        var settingName = settingNameMatch.Value.Trim('[', ']');
        return settingName;
      }

      return string.Empty;
    }
  }
}