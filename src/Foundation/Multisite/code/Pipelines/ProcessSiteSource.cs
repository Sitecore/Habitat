using Sitecore.Diagnostics;
using Sitecore.Foundation.Multisite.Providers;
using Sitecore.Pipelines.GetLookupSourceItems;

namespace Sitecore.Foundation.Multisite.Pipelines
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ProcessSiteSource
    {
        private const string QueryDatasourcePrefix = "query:";
        private const string QueryDatasourceMatchPattern = @"^" + QueryDatasourcePrefix + @"(.*)$";
        private readonly IFieldDatasourceProvider provider;

        public ProcessSiteSource() : this(new FieldDatasourceProvider())
        {
        }

        public ProcessSiteSource(IFieldDatasourceProvider provider)
        {
            this.provider = provider;
        }

        public void Process(GetLookupSourceItemsArgs args)
        {
            Assert.ArgumentNotNull((object)args, "args");


            var datasource = this.RemoveQueryFromDatasourceString(args.Source);
            if (!DatasourceConfigurationService.IsSiteDatasourceLocation(datasource))
            {
                return;
            }

            this.ResolveDatasource(args);
        }

        protected virtual void ResolveDatasource(GetLookupSourceItemsArgs args)
        {
            var datasource = this.RemoveQueryFromDatasourceString(args.Source);
            var name = DatasourceConfigurationService.GetSiteDatasourceConfigurationName(datasource);
            var datasourceLocations = this.provider.GetDatasourceLocation(args.Item, name);

            var datasourceLocation = datasourceLocations.FirstOrDefault();

            args.Source = datasourceLocation?.Paths?.FullPath ?? string.Empty;
        }

        //this is a hack to support treelist fields that only run the GetLookupSourceItems pipeline if the source contains a query:
        private string RemoveQueryFromDatasourceString(string datasourceLocationValue)
        {
            var match = Regex.Match(datasourceLocationValue, QueryDatasourceMatchPattern);
            return !match.Success ? datasourceLocationValue : match.Groups[1].Value;
        }
    }
}