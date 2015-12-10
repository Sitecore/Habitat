namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Web;

  public class ConfigurationSiteDefinitionsProvider : SiteDefinitionsProviderBase
  {
    public override IEnumerable<SiteDefinitionItem> SiteDefinitions
    {
      get
      {
        var sites = Sitecore.Configuration.Factory.GetSiteInfoList();

        return sites.Select(siteInfo => new SiteDefinitionItem {HostName = siteInfo.HostName, Name = siteInfo.Name});
      }
    }
  }
}