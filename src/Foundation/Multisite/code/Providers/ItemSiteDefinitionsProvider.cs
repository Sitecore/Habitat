namespace Sitecore.Foundation.MultiSite.Providers
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class ItemSiteDefinitionsProvider : SiteDefinitionsProviderBase
  {
    public override IEnumerable<SiteDefinition> SiteDefinitions
    {
      get
      {
        var contentRoot = Context.Database.GetItem(ItemIDs.ContentRoot);
        return contentRoot?.Children.Where(IsSite).Select(Create);
      }
    }

    public SiteDefinition Create(Item item)
    {
      Assert.ArgumentNotNull(item, nameof(item));
      Assert.ArgumentCondition(IsSite(item), nameof(item), $"Invalid item type, must derive from {Templates.Site.ID}");
      return new SiteDefinition
             {
               Item = item,
               Name = item.Name,
               HostName = item[Templates.Site.Fields.HostName],
               IsCurrent = IsCurrent(item.Name)
             };
    }

    public static bool IsSite(Item item)
    {
      return item.IsDerived(Templates.Site.ID);
    }

    public Item GetSiteItemBySiteContext()
    {
      return GetSiteItemByHierarchy(Context.Site.GetStartItem());
    }

    public Item GetSiteItemByHierarchy(Item item)
    {
      return item.Axes.GetAncestors().FirstOrDefault(IsSite);
    }

    public override SiteDefinition GetContextSiteDefinition(Item item)
    {
      var siteItem = GetSiteItemByHierarchy(item) ?? GetSiteItemBySiteContext();
      return siteItem == null ? null : Create(siteItem);
    }
  }
}