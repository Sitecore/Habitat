namespace Habitat.Search.Models
{
  using Habitat.Framework.Indexing.Models;
  using Sitecore.Data.Items;

  public class SearchSettings : ISearchSettings
  {
    public Item ConfigurationItem { get; set; }
    public string Query { get; set; }
    public string SearchBoxTitle { get; set; }
    public string SearchResultsUrl { get; set; }
    public Item Root { get; set; }
  }
}