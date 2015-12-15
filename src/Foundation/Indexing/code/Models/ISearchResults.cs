namespace Sitecore.Foundation.Indexing.Models
{
  using System.Collections.Generic;

  public interface ISearchResults
  {
    IEnumerable<ISearchResult> Results { get; }
    int TotalNumberOfResults { get; }
  }
}