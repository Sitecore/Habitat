using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Pipelines
{
  using System.Text.RegularExpressions;
  using Sitecore.Collections;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Pipelines.GetLookupSourceItems;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class SiteDataSource
  {
    public const string SiteSourceMatchPattern = "^\\$site\\[\\w*\\]$"; 
    public void Process(GetRenderingDatasourceArgs args)
    {
      var source = args.RenderingItem["Datasource Location"];
      Assert.ArgumentNotNull((object)args, "args");
      if (!source.StartsWith("$site"))
      {
        return;
      }
      this.ResolveSources(args);
    }

    private void ResolveSources(GetRenderingDatasourceArgs args)
    {
      
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var source = args.RenderingItem["Datasource Location"];
      var name = this.GetSourceSettingName(source);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      var provider = new ItemDatasourceProvider(args.ContentDatabase);
      var sources = provider.GetSources(name, contextItem);

      args.DatasourceRoots.AddRange(sources);
    }

    public string GetSourceSettingName(string source)
    {
      var match = Regex.Match(source, SiteSourceMatchPattern);
      if (match.Success)
      {
        var value = match.Value;
        var settingNameMatch = Regex.Match(value, "\\[\\w*\\]");
        if (settingNameMatch.Success)
        {
          var settingName = settingNameMatch.Value.Trim('[', ']');
          return settingName;
        }
      }

      return string.Empty;
    }
  }
}