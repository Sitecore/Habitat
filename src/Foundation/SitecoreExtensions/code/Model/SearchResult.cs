namespace Sitecore.Foundation.SitecoreExtensions.Model
{
  using System.Collections.Generic;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;

  public class SearchResult : SearchResultItem
  {
    [IndexField("all_templates")]
    public List<string> Templates { get; set; }
    [IndexField("_latestversion")]
    public bool IsLatestVersion { get; set; }
  }
}