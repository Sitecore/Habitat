namespace Sitecore.Feature.Media.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Feature.Media.Models;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Extensions;

  public static class MediaSelectorElementsRepository
  {
    public static IEnumerable<MediaSelectorElement> Get(Item item)
    {
      var active = "active";
      var multiListValues = item.GetMultiListValueItems(Templates.HasMediaSelector.Fields.MediaSelector);
      var mediaItems = multiListValues.Where(i => i.IsDerived(Templates.HasMedia.ID));
      foreach (var child in mediaItems)
      {
        if (child.IsDerived(Templates.HasMediaVideo.ID) && child[Templates.HasMediaVideo.Fields.VideoLink].IsEmptyOrNull() && child[Templates.HasMedia.Fields.Thumbnail].IsEmptyOrNull())
        {
          continue;
        }

        yield return new MediaSelectorElement
                     {
                       Item = child,
                       Active = active
                     };
        active = "";
      }
    }
  }
}