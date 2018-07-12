namespace Sitecore.Feature.FAQ.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public static class FaqRepository
  {
    public static IEnumerable<Item> Get([NotNull] Item item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(item));

      return item.GetMultiListValueItems(Templates.FaqGroup.Fields.GroupMember).Where(i => i.IsDerived(Templates.Faq.ID));
    }
  }
}