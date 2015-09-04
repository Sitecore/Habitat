using System.Collections.Generic;
using System.Linq;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Habitat.StandardContent.Infrastructure.Repositories
{
    public static class AccordeonElementsRespository
    {
        public static IEnumerable<Item> Get(Item item)
        {
            Assert.IsNotNull(item, "item");
            return item.GetMultiListValues(Templates.HasAccordeon.Fields.AccordeonSelector)
                .Where(i => i.IsDerived(Templates.Teaser));
        }
    }
}