namespace Sitecore.Foundation.Multisite
{
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Reflection;

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

    public virtual IDatasourceProvider GetProviderByConfigPath(string path)
    {
      var configNode = Factory.GetConfigNode(path);
      if (configNode != null)
      {
        var provider = ReflectionUtil.CreateObject(configNode) as IDatasourceProvider;
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