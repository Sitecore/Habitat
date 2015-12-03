namespace Sitecore.Feature.Search.Models
{
  using System.Collections.Generic;
  using Sitecore.Data.Items;

  public class SearchResults
  {
    public string Query { get; set; }
    public Item ConfigurationItem { get; set; }
    public IEnumerable<SearchResult> Results { get; set; }
  }
}