namespace Sitecore.Foundation.MultiSite.Providers
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Web;

  public class ItemSiteDefinitionsProvider : SiteDefinitionsProviderBase
  {
    public override IEnumerable<SiteDefinition> SiteDefinitions
    {
      get
      {
        var contentRoot = Context.Database.GetItem(Sitecore.ItemIDs.ContentRoot);
        return contentRoot?.Children.Where(SiteContext.IsSite).Select(CreateSiteDefinition);
      }
    }

    private SiteDefinition CreateSiteDefinition(Item siteItem)
    {
      return new SiteDefinition
             {
               Item = siteItem,
               Name = siteItem.Name,
               HostName = siteItem[Templates.Site.Fields.HostName],
               IsCurrent = this.IsCurrent(siteItem.Name)
             };
    }
  }
}