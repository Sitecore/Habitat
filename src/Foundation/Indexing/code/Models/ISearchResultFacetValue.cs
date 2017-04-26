namespace Sitecore.Foundation.Indexing.Models
{
    public interface ISearchResultFacetValue
    {
        string Title { get; set; }
        object Value { get; }
        int Count { get; }
        bool Selected { get; set; }
    }
}