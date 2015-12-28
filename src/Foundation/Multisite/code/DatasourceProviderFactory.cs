using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite
{
  using System.Xml;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Foundation.MultiSite.Providers;

  public class DatasourceProviderFactory
  {
    private const string DatasourceProviderConfigNodePath = "multisite/datasourceProvider";
    private const string FallbackDatasourceProviderConfigNodePath = "multisite/datasourceProvider/fallback";

    public virtual IDatasourceProvider GetProvider(Database database)
    {
      var provider = this.GetProviderByConfigPath(DatasourceProviderConfigNodePath);
      if (provider != null)
      {
        provider.Database = database;
      }
      return provider;
    }

    protected virtual IDatasourceProvider GetProviderByConfigPath(string path)
    {
      var configNode = Sitecore.Configuration.Factory.GetConfigNode(path);
      if (configNode != null)
      {
        var provider = Sitecore.Reflection.ReflectionUtil.CreateObject(configNode) as IDatasourceProvider;
        return provider;
      }

      return null;
    }


    public virtual IDatasourceProvider GetFallbackProvider(Database database)
    {
      var provider = this.GetProviderByConfigPath(FallbackDatasourceProviderConfigNodePath);
      if (provider != null)
      {
        provider.Database = database;
      }

      return provider;
    }

  }
}