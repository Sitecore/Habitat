using System.Web;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines.ItemProvider.GetRootItem;

namespace Habitat.Search.Models
{
    internal class SearchSettingsRepository
    {
        public static SearchSettings Get()
        {
            var configurationItem = RenderingContext.Current.Rendering.Item;
            if (configurationItem == null || !configurationItem.IsDerived(Templates.SearchResults.ID))
                return null;

            return new SearchSettings
            {
                ConfigurationItem = configurationItem,
                Query = HttpContext.Current == null ? "" : HttpContext.Current.Request["query"],
                SearchBoxTitle = configurationItem[Templates.SearchResults.Fields.SearchBoxTitle],
                SearchResultsUrl = configurationItem.Url(),
                Root = GetRootItem(configurationItem)
            };
        }

        private static Item GetRootItem(Item configurationItem)
        {
            Item rootItem = null;
            if (configurationItem.Fields[Templates.SearchResults.Fields.Root].HasValue)
            {
                rootItem = ((ReferenceField) configurationItem.Fields[Templates.SearchResults.Fields.Root]).TargetItem;
            }
            return rootItem ?? (Sitecore.Context.Site.GetRoot());
        }
    }
}