namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections.Generic;

    public interface IQuery
    {
        string QueryText { get; set; }
        int IndexOfFirstResult { get; set; }
        int NoOfResults { get; set; }
        Dictionary<string, string[]> Facets { get; set; }
    }
}