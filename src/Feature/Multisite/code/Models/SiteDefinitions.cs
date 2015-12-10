namespace Sitecore.Feature.Multisite.Models
{
  using System.Collections.Generic;
  using System.Linq;

  public class SiteDefinitions
  {
    public IEnumerable<SiteConfiguration> Sites { get; set; }

    public SiteConfiguration Current
    {
      get
      {
        return this.Sites.FirstOrDefault(site => site.IsCurrent);
      }
    }
  }
}