using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Multisite.Models
{
  public class SiteDefinition
  {
    public string Name { get; set; }
    public string HostName { get; set; }

    public bool IsCurrent { get; set; }
  }
}