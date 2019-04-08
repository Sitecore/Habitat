namespace Sitecore.Feature.Search.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Buckets.Util;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Search.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Presentation;

    [Service(typeof(ISearchContextRepository))]
    public class SearchContextRepository : ISearchContextRepository
    {
        private const string DefaultSearchResultsName = "search";

        public virtual SearchContext Get()
        {
            var query = HttpContext.Current == null ? "" : HttpContext.Current.Request["query"];

            var searchResultsPageItem = GetSearchResultsPageItem();
            if (searchResultsPageItem == null)
            {
                return null;
            }
            return new SearchContext
            {
                ConfigurationItem = searchResultsPageItem,
                Query = query,
                SearchBoxTitle = searchResultsPageItem[Templates.SearchResults.Fields.SearchBoxTitle],
                SearchResultsUrl = searchResultsPageItem.Url(),
                Root = this.GetRootItem(searchResultsPageItem),
                Facets = this.GetFacets(searchResultsPageItem).ToArray()
            };
        }

        private IEnumerable<IQueryFacet> GetFacets(Item configurationItem)
        {
            var facets = new List<IQueryFacet>();
            if (!configurationItem.FieldHasValue(Templates.SearchResults.Fields.Facets))
            {
                return facets;
            }

            var facetItems = ((MultilistField)configurationItem.Fields[Templates.SearchResults.Fields.Facets]).GetItems();
            foreach (var item in facetItems)
            {
                var facet = new QueryFacet
                {
                    Title = item[Constants.FacetDisplayName],
                    FieldName = item[Constants.FacetParameters].ToLowerInvariant()
                };

                facets.Add(facet);
            }
            return facets.Distinct();
        }

        private class QueryFacet : IQueryFacet
        {
            public string Title { get; set; }
            public string FieldName { get; set; }
            public string ViewName { get; set; }
        }

        private static Item GetSearchResultsPageItem()
        {
            return GetSearchResultsPageItemFromRenderingContext() ??
                   GetSearchResultsPageItemFromContext() ??
                   GetDefaultSearchResultsPage();
        }

        private static Item GetDefaultSearchResultsPage()
        {
            var item = Context.Site?.GetStartItem().Children[DefaultSearchResultsName];
            return item != null && item.DescendsFrom(Templates.SearchResults.ID) ? item : null;
        }

        private static Item GetSearchResultsPageItemFromContext()
        {
            var item = Context.Item?.GetAncestorOrSelfOfTemplate(Templates.SearchContext.ID) ?? Context.Site?.GetContextItem(Templates.SearchContext.ID);
            if (item == null)
            {
                return null;
            }
            var searchResultsItem = item.TargetItem(Templates.SearchContext.Fields.SearchResultsPage);
            return searchResultsItem != null && searchResultsItem.DescendsFrom(Templates.SearchResults.ID) ? searchResultsItem : null;
        }

        private static Item GetSearchResultsPageItemFromRenderingContext()
        {           
            var item = RenderingContext.CurrentOrNull?.Rendering.Item;
            return item != null && item.DescendsFrom(Templates.SearchResults.ID) ? item : null;
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