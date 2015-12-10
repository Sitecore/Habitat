using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Multisite
{
  using Sitecore.Data.Items;

  public class SiteDefinitionItem
  {
    public Item Item { get; set; }
    public string HostName { get; set; }

    public string Name { get; set; }
  }
}