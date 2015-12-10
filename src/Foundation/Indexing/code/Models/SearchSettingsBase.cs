using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Indexing.Models
{
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public class SearchSettingsBase : ISearchSettings
  {
    public Item Root { get; set; }

    public IEnumerable<ID> Templates { get; set; }
  }
}