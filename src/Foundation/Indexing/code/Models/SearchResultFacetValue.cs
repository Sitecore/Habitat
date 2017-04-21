namespace Sitecore.Foundation.Indexing.Models
{
    internal class SearchResultFacetValue : ISearchResultFacetValue
    {
        public object Value { get; set; }
        public int Count { get; set; }
        public bool Selected { get; set; }
    }
}