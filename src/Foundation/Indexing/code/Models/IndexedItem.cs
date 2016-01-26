namespace Sitecore.Foundation.Indexing.Models
{
  using System.Collections.Generic;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;

  public class IndexedItem : SearchResultItem
  {
    [IndexField(Constants.IndexFields.HasPresentation)]
    public bool HasPresentation { get; set; }

    [IndexField(Templates.IndexedItem.Fields.IncludeInSearchResults_FieldName)]
    public bool ShowInSearchResults { get; set; }

    [IndexField(Constants.IndexFields.AllTemplates)]
    public List<string> AllTemplates { get; set; }

    [IndexField(Constants.IndexFields.IsLatestVersion)]
    public bool IsLatestVersion { get; set; }
  }
}