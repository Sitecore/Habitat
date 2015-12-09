namespace Sitecore.Feature.Search.Models
{
  using System.Collections.Generic;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Models;

  public class SearchSettings : ISearchSettings
  {
    public Item ConfigurationItem { get; set; }
    public string Query { get; set; }
    public string SearchBoxTitle { get; set; }
    public string SearchResultsUrl { get; set; }
    public Item Root { get; set; }

    public IEnumerable<ID> Templates { get; set; }
  }
}