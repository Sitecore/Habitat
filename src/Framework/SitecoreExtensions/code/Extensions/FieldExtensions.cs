namespace Habitat.Framework.SitecoreExtensions.Extensions
{
  using System;
  using Sitecore;
  using Sitecore.Data.Fields;
  using Sitecore.Resources.Media;

  public static class FieldExtensions
  {
    public static string ImageUrl(this ImageField imageField, MediaUrlOptions options = null)
    {
      if (imageField?.MediaItem == null)
      {
        throw new ArgumentNullException(nameof(imageField));
      }

      if (options != null)
      {
        return HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(imageField.MediaItem, options));
      }

      options = MediaUrlOptions.Empty;
      int width, height;

      if (int.TryParse(imageField.Width, out width))
      {
        options.Width = width;
      }

      if (int.TryParse(imageField.Height, out height))
      {
        options.Height = height;
      }
      return HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(imageField.MediaItem, options));
    }

    public static bool IsChecked(this Field checkboxField)
    {
      if (checkboxField == null)
      {
        throw new ArgumentNullException(nameof(checkboxField));
      }
      return MainUtil.GetBool(checkboxField.Value, false);
    }
  }
}