namespace Sitecore.Foundation.Multisite.Pipelines
{
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class GetDatasourceLocationAndTemplateFromSite
  {
    //Sitecore has no constant in FieldIDs for this standard field
    private const string DatasourceLocationFieldName = "Datasource Location";
    private readonly IDatasourceProvider provider;

    public GetDatasourceLocationAndTemplateFromSite() : this(new DatasourceProvider())
    {
    }

    public GetDatasourceLocationAndTemplateFromSite(IDatasourceProvider provider)
    {
      this.provider = provider;
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
      var source = args.RenderingItem[DatasourceLocationFieldName];
      var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(source);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      var datasourceLocations = this.provider.GetDatasourceLocations(contextItem, name);
      args.DatasourceRoots.AddRange(datasourceLocations);
    }

    protected virtual void ResolveDatasourceTemplate(GetRenderingDatasourceArgs args)
    {
      var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
      var datasourceLocation = args.RenderingItem[DatasourceLocationFieldName];
      var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocation);
      if (string.IsNullOrEmpty(name))
      {
        return;
      }

      args.Prototype = this.provider.GetDatasourceTemplate(contextItem, name);
        }
      }
}