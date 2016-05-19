namespace Sitecore.Foundation.Multisite.Pipelines
{
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class GetDatasourceLocationAndTemplateFromSite
  {
    private const string DatasourceLocationFieldName = "Datasource Location";
    private readonly DatasourceProviderFactory providerFactory;

    public GetDatasourceLocationAndTemplateFromSite() : this(new DatasourceProviderFactory())
    {
    }

    public GetDatasourceLocationAndTemplateFromSite(DatasourceProviderFactory factory)
    {
      this.providerFactory = factory;
    }

    public void Process(GetRenderingDatasourceArgs args)
    {
      Assert.ArgumentNotNull(args, nameof(args));

      var datasource = args.RenderingItem[DatasourceLocationFieldName];
      if (!DatasourceConfigurationService.IsSiteDatasourceLocation(datasource))
      {
        return;
      }

      this.ResolveDatasource(args);
      this.ResolveDatasourceTemplate(args);
    }

    protected virtual void ResolveDatasource(GetRenderingDatasourceArgs args)
    {
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var source = args.RenderingItem["Datasource Location"];
      var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(source);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      var datasources = new Item[] {};
      var provider = this.providerFactory.GetProvider(args.ContentDatabase);
      if (provider != null)
      {
        datasources = provider.GetDatasources(name, contextItem);
      }

      if (!datasources.Any())
      {
        provider = this.providerFactory.GetFallbackProvider(args.ContentDatabase);
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
      var datasourceLocation = args.RenderingItem["Datasource Location"];
      var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocation);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      Item datasourceTemplate = null;
      var provider = this.providerFactory.GetProvider(args.ContentDatabase);
      if (provider != null)
      {
        datasourceTemplate = provider.GetDatasourceTemplate(name, contextItem);
      }

      if (datasourceTemplate == null)
      {
        provider = this.providerFactory.GetFallbackProvider(args.ContentDatabase);
        if (provider != null)
        {
          datasourceTemplate = provider.GetDatasourceTemplate(name, contextItem);
        }
      }

      args.Prototype = datasourceTemplate;
    }
  }
}