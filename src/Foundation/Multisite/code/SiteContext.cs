using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite
{
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class SiteContext
  {
    public virtual SiteDefinition GetSiteDefinition(Item item)
    {
      Assert.ArgumentNotNull(item, nameof(item));

      var siteItem = GetSiteItemByHierarchy(item) ?? GetSiteItemBySiteContext();
      if (siteItem == null)
        return null;

      return new SiteDefinition
      {
        Item = siteItem,
        Name = siteItem.Name,
        HostName = siteItem[Templates.Site.Fields.HostName]
      };
    }

    private Item GetSiteItemBySiteContext()
    {
      return GetSiteItemByHierarchy(Context.Site.GetStartItem());
    }

    private static Item GetSiteItemByHierarchy(Item item)
    {
      return item.Axes.GetAncestors().FirstOrDefault(IsSite);
    }

    public static bool IsSite(Item item)
    {
      return item.IsDerived(Templates.Site.ID);
    }
  }
}