namespace Habitat.Media.Infrastructure.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Habitat.Media.Infrastructure.Models;
  using Sitecore.Data.Items;
  using Sitecore.Mvc.Extensions;

  public static class CarouselElementsRepository
  {
    public static IEnumerable<CarouselElement> Get(Item item)
    {
      var active = "active";
      var multiListValues = item.GetMultiListValues(Templates.HasMediaSelector.Fields.MediaSelector);
      var mediaItems = multiListValues.Where(i => i.IsDerived(Templates.HasMedia.ID));
      foreach (var child in mediaItems)
      {
        if (child.IsDerived(Templates.HasMediaVideo.ID) 
          && (child[Templates.HasMediaVideo.Fields.VideoLink].IsEmptyOrNull()
          && child[Templates.HasMedia.Fields.Thumbnail].IsEmptyOrNull()))
        {
          continue;
        }

        yield return new CarouselElement
        {
          Item = child,
          Active = active
        };
        active = "";
      }
    }
  }
}