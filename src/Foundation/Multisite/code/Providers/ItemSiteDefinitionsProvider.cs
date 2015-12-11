namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Web;

  public class ItemSiteDefinitionsProvider : SiteDefinitionsProviderBase
  {
    public override IEnumerable<SiteDefinitionItem> SiteDefinitions
    {
      get
      {
        var contentRoot = Sitecore.Context.Database.GetItem(Sitecore.ItemIDs.ContentRoot);
        if (contentRoot != null)
        {
          return contentRoot.Children.Where(item => this.IsSite(item)).Select(siteItem => new SiteDefinitionItem
          {
            Item = siteItem,
            Name = siteItem.Name,
            HostName = siteItem[Templates.Site.Fields.HostName],
            IsCurrent = this.IsCurrent(siteItem.Name)
          });
        }

        return null;
      }
    }

    private bool IsSite(Item item)
    {
      return item.IsDerived(Templates.Site.ID);
    }
  }
}