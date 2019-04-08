namespace Sitecore.Feature.Multisite.Models
{
  using System.Collections.Generic;
  using System.Linq;

  public class SiteConfigurations
  {
    public IEnumerable<SiteConfiguration> Items { get; set; }

    public SiteConfiguration Current
    {
      get
      {
        return this.Items.FirstOrDefault(site => site.IsCurrent);
      }
    }
  }
}