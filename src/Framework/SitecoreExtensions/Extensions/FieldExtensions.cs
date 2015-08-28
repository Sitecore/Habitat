using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;

namespace Habitat.Framework.SitecoreExtensions.Extensions
{
    public static class FieldExtensions
    {
        public static string ImageUrl(this ImageField imageField, MediaUrlOptions options = null)
        {
            if (imageField?.MediaItem == null)
            {
                throw new ArgumentNullException(nameof(imageField));
            }

            if (options != null)
                return HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(imageField.MediaItem, options));

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
    }
}
