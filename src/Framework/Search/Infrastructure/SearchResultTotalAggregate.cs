using System.Collections.Generic;

namespace Habitat.Framework.Indexing.Infrastructure
{
    internal class SearchResultTotalAggregate
    {
        public string QueryWord { get; set; }
        public string TotalCountOfResults { get; set; }
        public IEnumerable<CustomSearchResult> SearchResults { get; set; }
    }
}