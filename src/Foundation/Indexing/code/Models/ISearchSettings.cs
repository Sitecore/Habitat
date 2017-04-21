namespace Sitecore.Foundation.Indexing.Models
{
  using System.Collections.Generic;
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public interface ISearchSettings : IQueryRoot
  {
    IEnumerable<ID> Templates { get; set; }
  }
}