namespace Sitecore.Foundation.Indexing.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;

    [Service]
    public class SearchResultsFactory
    {
        public ISearchResults Create(SearchResults<SearchResultItem> results, IQuery query)
        {
            var searchResults = this.CreateSearchResults(results);
            return new SearchResults
            {
                Results = searchResults,
                TotalNumberOfResults = results.TotalSearchResults,
                Query = query
            };
        }

        private IEnumerable<ISearchResult> CreateSearchResults(SearchResults<SearchResultItem> results)
        {
            return results.Hits.Select(h => SearchResultFactory.Create(h.Document)).ToArray();
        }
    }
}