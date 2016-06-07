namespace Sitecore.Foundation.Multisite.Pipelines
{
  using System.Linq;
  using System.Text.RegularExpressions;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class GetDatasourceLocationAndTemplateFromSite
  {
    private readonly DatasourceProviderFactory providerFactory;

    public GetDatasourceLocationAndTemplateFromSite() : this(new DatasourceProviderFactory())
    {
    }

    public GetDatasourceLocationAndTemplateFromSite(DatasourceProviderFactory factory)
    {
      providerFactory = factory;
    }

    public void Process(GetRenderingDatasourceArgs args)
    {
      Assert.ArgumentNotNull(args, nameof(args));

      var datasource = args.RenderingItem[Templates.DatasourceConfiguration.Fields.DatasourceLocation];
      if (!DatasourceConfigurationService.IsSiteDatasourceLocation(datasource))
      {
        return;
      }

      ResolveDatasource(args);
      ResolveDatasourceTemplate(args);
    }

    protected virtual void ResolveDatasource(GetRenderingDatasourceArgs args)
    {
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var source = args.RenderingItem[Templates.DatasourceConfiguration.Fields.DatasourceLocation];
      var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(source);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      var datasources = new Item[] {};
      var provider = providerFactory.GetProvider(args.ContentDatabase);
      if (provider != null)
      {
        datasources = provider.GetDatasources(name, contextItem);
      }

      if (!datasources.Any())
      {
        provider = providerFactory.GetFallbackProvider(args.ContentDatabase);
        if (provider == null)
        {
          return;
        }

        datasources = provider.GetDatasources(name, contextItem);
      }

      args.DatasourceRoots.AddRange(datasources);
    }

    protected virtual void ResolveDatasourceTemplate(GetRenderingDatasourceArgs args)
    {
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var datasourceLocation = args.RenderingItem[Templates.DatasourceConfiguration.Fields.DatasourceLocation];
      var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocation);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      Item datasourceTemplate = null;
      var provider = providerFactory.GetProvider(args.ContentDatabase);
      if (provider != null)
      {
        datasourceTemplate = provider.GetDatasourceTemplate(name, contextItem);
      }

      if (datasourceTemplate == null)
      {
        provider = providerFactory.GetFallbackProvider(args.ContentDatabase);
        if (provider != null)
        {
          datasourceTemplate = provider.GetDatasourceTemplate(name, contextItem);
        }
      }

      args.Prototype = datasourceTemplate;
    }

  }
}