using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Framework.Indexing.Models
{
  public interface IPageble
  {
    int TotalPagesCount { get; }

    string Query { get; set; }

    int Page { get; set; }

    int TotalResults { get; set; }

    int ResultsOnPage { get; set; }

    int FirstPage { get; }

    int LastPage { get; }

    ISearchResults Results { get; set; }
  }
}