using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;

namespace Habitat.Framework.Indexing.Infrastructure.Services
{
    internal class SearchService
    {
        private readonly ISearchIndex _index;

        public SearchService(ISearchIndex index)
        {
            _index = index;
        }

        public SearchResultTotalAggregate Search(string queryWord, int queryPage)
        {
            var searchResultTotalAggregate = new SearchResultTotalAggregate
            {
                SearchResults = new List<CustomSearchResult>(),
                QueryWord = queryWord
            };

            using (var context = _index.CreateSearchContext())
            {
                var searchResults = context.GetQueryable<CustomSearchResult>()
                    .Where(item => item.Content.Contains(queryWord))
                    .Page(queryPage, 20)
                    .GetResults();

                searchResultTotalAggregate.TotalCountOfResults = searchResults.TotalSearchResults.ToString();
                searchResultTotalAggregate.SearchResults = searchResults.Select(r => r.Document).ToList();
            }
            return searchResultTotalAggregate;
        }
    }
}