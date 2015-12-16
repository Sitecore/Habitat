namespace Sitecore.Feature.MultiSite.Models
{
  using System.Collections.Generic;
  using System.Linq;

  public class SiteDefinitions
  {
    public IEnumerable<SiteDefinition> Sites { get; set; }

    public SiteDefinition Current
    {
      get
      {
        return this.Sites.FirstOrDefault(site => site.IsCurrent);
      }
    }
  }
}