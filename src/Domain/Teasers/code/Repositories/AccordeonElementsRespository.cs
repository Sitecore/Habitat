namespace Sitecore.Feature.Teasers.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Framework.SitecoreExtensions.Extensions;

  public static class AccordeonElementsRespository
  {
    public static IEnumerable<Item> Get(Item item)
    {
      Assert.IsNotNull(item, "item");
      return item.GetMultiListValues(Templates.Accordeon.Fields.AccordeonSelector).Where(i => i.IsDerived(Templates.TeaserContent.ID));
    }
  }
}