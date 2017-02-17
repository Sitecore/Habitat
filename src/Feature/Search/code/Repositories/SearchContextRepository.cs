namespace Sitecore.Feature.Search.Repositories
{
    using System.Web;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Search.Models;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Presentation;

    public class SearchContextRepository : ISearchContextRepository
    {
        private const string DefaultSearchResultsName = "search";

        public virtual SearchContext Get()
        {
            var query = HttpContext.Current == null ? "" : HttpContext.Current.Request["query"];

            var searchResultsPageItem = GetSearchResultsPageItem();
            return new SearchContext
            {
                ConfigurationItem = searchResultsPageItem,
                Query = query,
                SearchBoxTitle = searchResultsPageItem[Templates.SearchResults.Fields.SearchBoxTitle],
                SearchResultsUrl = searchResultsPageItem.Url(),
                Root = this.GetRootItem(searchResultsPageItem)
            };
        }

        private static Item GetSearchResultsPageItem()
        {
            return GetSearchResultsPageItemFromRenderingContext() ??
                   GetSearchResultsPageItemFromContext() ??
                   GetDefaultSearchResultsPage();
        }

        private static Item GetDefaultSearchResultsPage()
        {
            var item = Context.Site.GetStartItem().Children[DefaultSearchResultsName];
            return item != null && item.IsDerived(Templates.SearchResults.ID) ? item : null;
        }

        private static Item GetSearchResultsPageItemFromContext()
        {
            var item = Context.Item.GetAncestorOrSelfOfTemplate(Templates.SearchContext.ID) ?? Context.Site.GetContextItem(Templates.SearchContext.ID);
            if (item == null)
                return null;
            var searchResultsItem = item.TargetItem(Templates.SearchContext.Fields.SearchResultsPage);
            return searchResultsItem != null && searchResultsItem.IsDerived(Templates.SearchResults.ID) ? searchResultsItem : null;
        }

        private static Item GetSearchResultsPageItemFromRenderingContext()
        {
            var item = RenderingContext.Current?.Rendering.Item;
            return item != null && item.IsDerived(Templates.SearchResults.ID) ? item : null;
        }

        private Item GetRootItem(Item configurationItem)
        {
            Item rootItem = null;
            if (configurationItem.FieldHasValue(Templates.SearchResults.Fields.Root))
            {
                rootItem = configurationItem.TargetItem(Templates.SearchResults.Fields.Root);
            }
            return rootItem ?? Context.Site.GetRootItem();
        }
    }
}