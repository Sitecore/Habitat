namespace Sitecore.Foundation.Multisite.Providers
{
    using System;
    using Sitecore.Data.Items;

    public class FieldDatasourceProvider : IFieldDatasourceProvider
    {
        private readonly ISiteSettingsProvider siteSettingsProvider;
        private const string DatasourceSettingsName = "field datasources";
        private const string QueryPrefix = "query:";

        public FieldDatasourceProvider() : this(new SiteSettingsProvider())
        {
        }

        public FieldDatasourceProvider(ISiteSettingsProvider siteSettingsProvider)
        {
            this.siteSettingsProvider = siteSettingsProvider;
        }

        public Item[] GetDatasourceLocation(Item contextItem, string datasourceName)
        {
            var sourceSettingItem = this.siteSettingsProvider.GetSetting(contextItem, DatasourceSettingsName, datasourceName);
            var datasourceRoot = sourceSettingItem?[Templates.FieldDatasourceConfiguration.Fields.FieldDatasourceLocation];

            if (string.IsNullOrEmpty(datasourceRoot))
                return new Item[] { };

            if (datasourceRoot.StartsWith(QueryPrefix, StringComparison.InvariantCulture))
            {
                return this.GetRootsFromQuery(contextItem, datasourceRoot.Substring(QueryPrefix.Length));
            }
            if (datasourceRoot.StartsWith("./", StringComparison.InvariantCulture))
            {
                return this.GetRelativeRoots(contextItem, datasourceRoot);
            }

            var sourceRootItem = contextItem.Database.GetItem(datasourceRoot);
            return sourceRootItem != null ? new[] { sourceRootItem } : new Item[] { };
        }

        private Item[] GetRelativeRoots(Item contextItem, string relativePath)
        {
            var path = contextItem.Paths.FullPath + relativePath.Remove(0, 1);
            var root = contextItem.Database.GetItem(path);
            return root != null ? new[] { root } : new Item[] { };
        }

        private Item[] GetRootsFromQuery([NotNull] Item contextItem, string query)
        {
            if (contextItem == null)
                throw new ArgumentNullException(nameof(contextItem));

            var roots = query.StartsWith("./", StringComparison.InvariantCulture) ? contextItem.Axes.SelectItems(query) : contextItem.Database.SelectItems(query);
            return roots;
        }
    }
}