namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections.Generic;

    public interface ISearchResultFacet
    {
        IQueryFacet Definition { get; set; }
        IEnumerable<ISearchResultFacetValue> Values { get; set; }
    }
}