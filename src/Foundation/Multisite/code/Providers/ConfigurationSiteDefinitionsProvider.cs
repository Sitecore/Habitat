namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Configuration;
  using Sitecore.Data.Items;
  using Sitecore.Web;

  public class ConfigurationSiteDefinitionsProvider : SiteDefinitionsProviderBase
  {
    public override IEnumerable<SiteDefinition> SiteDefinitions
    {
      get
      {
        var sites = Factory.GetSiteInfoList();
        return sites.Select(CreateSiteDefinition);
      }
    }

    private SiteDefinition CreateSiteDefinition(SiteInfo siteInfo)
    {
      return new SiteDefinition
             {
               HostName = siteInfo.HostName,
               Name = siteInfo.Name,
               IsCurrent = IsCurrent(siteInfo.Name)
             };
    }

    public override SiteDefinition GetContextSiteDefinition(Item item)
    {
      return CreateSiteDefinition(Sitecore.Context.Site.SiteInfo);
    }
  }
}