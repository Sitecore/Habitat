namespace Sitecore.Foundation.SitecoreExtensions.Rendering
{
  using System.Collections.Generic;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;

  internal class SearchResult : SearchResultItem
  {
    [IndexField("all_templates")]
    public List<string> Templates { get; set; }
    [IndexField("_latestversion")]
    public bool IsLatestVersion { get; set; }
  }
}