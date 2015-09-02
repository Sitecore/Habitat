using System.Collections.Generic;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Media.Infrastructure.Models;
using Sitecore.Data.Items;

namespace Habitat.Media.Infrastructure.Repositories
{
    public static class CarouselElementsRepository
    {
        public static IEnumerable<CarouselElement> Get(Item item, string linkClass = "", string imageClass = "",
            string textClass = "")
        {
            var active = "active";
            foreach (var child in item.GetMultiListValues(Templates.HasMediaSelector.Fields.MediaSelector))
            {
                if (!child.IsDerived(Templates.HasMediaImage.ID)) continue;
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