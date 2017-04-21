namespace Sitecore.Feature.Search.Models
{
    using System.Collections.Generic;
    using Sitecore.Foundation.Indexing.Models;

    public class SearchQuery : IQuery
    {
        public string QueryText { get; set; }
        public int IndexOfFirstResult { get; set; }
        public int NoOfResults { get; set; }
        public Dictionary<string, string[]> Facets { get; set; }
    }
}