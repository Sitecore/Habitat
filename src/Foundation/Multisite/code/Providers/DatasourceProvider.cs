namespace Sitecore.Foundation.Multisite.Providers
{
    using System;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.DependencyInjection;

    [Service(typeof(IDatasourceProvider))]
    public class DatasourceProvider : IDatasourceProvider
    {
        public const string DatasourceSettingsName = "datasources";
        private const string QueryPrefix = "query:";
        private readonly ISiteSettingsProvider siteSettingsProvider;

        public DatasourceProvider(ISiteSettingsProvider siteSettingsProvider)
        {
            this.siteSettingsProvider = siteSettingsProvider;
        }

        public Item[] GetDatasourceLocations(Item contextItem, string datasourceName)
        {
            var sourceSettingItem = this.siteSettingsProvider.GetSetting(contextItem, DatasourceSettingsName, datasourceName);
            var datasourceRoot = sourceSettingItem?[Templates.DatasourceConfiguration.Fields.DatasourceLocation];

            if (string.IsNullOrEmpty(datasourceRoot))
            {
                return new Item[] { };
            }

            if (datasourceRoot.StartsWith(QueryPrefix, StringComparison.InvariantCulture))
            {
                return this.GetRootsFromQuery(contextItem, datasourceRoot.Substring(QueryPrefix.Length));
            }
            if (datasourceRoot.StartsWith("./", StringComparison.InvariantCulture))
            {
                return this.GetRelativeRoots(contextItem, datasourceRoot);
            }

            var sourceRootItem = contextItem.Database.GetItem(datasourceRoot);
            return sourceRootItem != null ? new[] {sourceRootItem} : new Item[] { };
        }

        public Item GetDatasourceTemplate(Item contextItem, string settingName)
        {
            var settingItem = this.siteSettingsProvider.GetSetting(contextItem, DatasourceSettingsName, settingName);
            var templateId = settingItem?[Templates.DatasourceConfiguration.Fields.DatasourceTemplate];

            return string.IsNullOrEmpty(templateId) ? null : contextItem.Database.GetItem(templateId);
        }

        private Item[] GetRelativeRoots(Item contextItem, string relativePath)
        {
            var path = contextItem.Paths.FullPath + relativePath.Remove(0, 1);
            var root = contextItem.Database.GetItem(path);
            return root != null ? new[] {root} : new Item[] { };
        }

        private Item[] GetRootsFromQuery(Item contextItem, string query)
        {
            if (contextItem == null)
            {
                throw new ArgumentNullException(nameof(contextItem));
            }

            var roots = query.StartsWith("./", StringComparison.InvariantCulture) ? contextItem.Axes.SelectItems(query) : contextItem.Database.SelectItems(query);
            return roots;
        }
    }
}