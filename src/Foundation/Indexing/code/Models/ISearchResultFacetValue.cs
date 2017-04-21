namespace Sitecore.Foundation.Indexing.Models
{
    public interface ISearchResultFacetValue
    {
        object Value { get; }
        int Count { get; }
        bool Selected { get; set; }
    }
}