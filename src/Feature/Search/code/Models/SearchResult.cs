namespace Sitecore.Feature.Search.Models
{
  using Sitecore.ContentSearch.SearchTypes;

  public class SearchResult : SearchResultItem
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContentType { get; set; }
  }
}