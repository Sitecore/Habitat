namespace Sitecore.Foundation.Multisite.Pipelines
{
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Pipelines.GetRenderingDatasource;

    [Service]
    public class GetDatasourceLocationAndTemplateFromSite
    {
        //Sitecore has no constant in FieldIDs for this standard field
        private readonly IDatasourceProvider provider;

        public GetDatasourceLocationAndTemplateFromSite(IDatasourceProvider provider)
        {
            this.provider = provider;
        }

        public void Process(GetRenderingDatasourceArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));

            var datasource = args.RenderingItem[Templates.RenderingOptions.Fields.DatasourceLocation];
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
            var source = args.RenderingItem[Templates.RenderingOptions.Fields.DatasourceLocation];
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
            var datasourceLocation = args.RenderingItem[Templates.RenderingOptions.Fields.DatasourceLocation];
            var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(datasourceLocation);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            args.Prototype = this.provider.GetDatasourceTemplate(contextItem, name);
        }
    }
}