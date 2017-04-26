namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections.Generic;

    internal class SearchResultFacet : ISearchResultFacet
    {
        public IEnumerable<ISearchResultFacetValue> Values { get; set; }
        public IQueryFacet Definition { get; set; }
    }
}