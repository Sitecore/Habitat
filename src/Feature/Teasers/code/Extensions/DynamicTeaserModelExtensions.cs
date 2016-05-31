namespace Sitecore.Feature.Teasers.Extensions
{
  using System.Linq;
  using Sitecore.Data.Fields;
  using Sitecore.Feature.Teasers.Models;

  public static class DynamicTeaserModelExtensions
  {
    public static int GetMaxImageHeight(this DynamicTeaserModel model)
    {
      int maxHeight = 0;
      foreach (var element in model.Items.Where(x => x.Item != null))
      {
        int height;
        var field = Templates.TeaserContent.Fields.Image;
        if (int.TryParse(((ImageField)element.Item.Fields[field]).Height, out height) && height > maxHeight)
        {
          maxHeight = height;
        }
      }

      return maxHeight;
    }
  }
}