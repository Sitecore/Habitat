using System.Collections.Generic;

namespace Habitat.Framework.Indexing.Models
{
    internal class SearchResults : ISearchResults
    {
        public IEnumerable<ISearchResult> Results { get; set; }
        public int TotalNumberOfResults { get; set; }
        public IQuery Query { get; set; }
    }
}