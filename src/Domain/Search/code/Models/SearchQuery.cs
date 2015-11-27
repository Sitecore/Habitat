using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Search.Models
{
  using Habitat.Framework.Indexing.Models;

  public class SearchQuery
  {
    public string Query { get; set; }

    public int Page { get; set; }

    public int ResultsOnPage { get; set; }
  }
}