

namespace Sitecore.Feature.News
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using Sitecore.Foundation.Indexing.Models;

  public class Query : IQuery
  {
    public string QueryText { get; set; }

    public int IndexOfFirstResult { get; set; }

    public int NoOfResults { get; set; }
  }
}