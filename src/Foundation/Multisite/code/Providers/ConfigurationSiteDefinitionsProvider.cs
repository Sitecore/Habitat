namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Collections;
  using Sitecore.Web;

  public class ConfigurationSiteDefinitionsProvider : SiteDefinitionsProviderBase
  {
    public override IEnumerable<SiteDefinitionItem> SiteDefinitions
    {
      get
      {
        var sites = Sitecore.Configuration.Factory.GetSiteInfoList();
        var siteInf = new SiteInfo(new StringDictionary());
        return sites.Select(siteInfo => new SiteDefinitionItem {HostName = siteInfo.HostName, Name = siteInfo.Name, IsCurrent = this.IsCurrent(siteInfo.Name)});
      }
    }
  }
}