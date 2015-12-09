using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.News
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Models;

  public class SearchSettings : ISearchSettings
  {
    public Item Root { get; set; }

    public IEnumerable<ID> Tempaltes { get; set; }
  }
}