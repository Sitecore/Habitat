using System.Collections.Generic;
using System.Linq;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Media.Infrastructure.Models;
using Sitecore.Data.Items;

namespace Habitat.Media.Infrastructure.Repositories
{
    public class CarouselElementsRepository
    {
        public static IEnumerable<CarouselElement> Get(Item item)
        {
            var active = "active";
            foreach (var child in item.GetMultiListValues(Templates.HasMediaSelector.Fields.MediaSelector).Where(i => i.IsDerived(Templates.HasMediaImage.ID)))
            {
                yield return new CarouselElement
                {
                    Item = child,
                    Active = active
                };
                active = "false";
            }
        }
    }
}