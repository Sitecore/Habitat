namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IQueryFacetProvider
    {
        IEnumerable<IQueryFacet> GetFacets();
    }
}