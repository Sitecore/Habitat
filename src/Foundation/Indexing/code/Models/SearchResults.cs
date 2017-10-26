namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections.Generic;

    internal class SearchResults : ISearchResults
    {
        public IQuery Query { get; set; }
        public IEnumerable<ISearchResult> Results { get; set; }
        public int TotalNumberOfResults { get; set; }
        public IEnumerable<ISearchResultFacet> Facets { get; set; }
    }
}