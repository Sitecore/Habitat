using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Feature.Media.Infrastructure
{
  using System.Text;
  using System.Web;
  using System.Web.UI.WebControls;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public static class Extensions
  {
    public static HtmlString RenderParallaxMediaAttributes(this Item item)
    {
      var targetItem = item.TargetItem(Templates.HasParallaxBackground.Fields.BackgroundMedia);
      var targetMedia = new MediaItem(targetItem);
      var attributeDictionary = new Dictionary<string, string>();
      var mimeType = targetMedia.MimeType.Split('/');
      var mediaType = mimeType[0];
      var mediaFormat = mimeType[1];

      attributeDictionary["type"] = mediaType;
      if (mediaType == "video")
        attributeDictionary["format"] = mediaFormat;
        
      attributeDictionary[mediaType == "video" ? $"url-{mediaFormat}" : "url"] = item.MediaUrl(Templates.HasParallaxBackground.Fields.BackgroundMedia);
      var checkboxField = (Sitecore.Data.Fields.CheckboxField)item.Fields[Templates.HasParallaxBackground.Fields.IsParallaxEnabled];
      attributeDictionary["attachment"] = checkboxField.Checked ? "parallax" : "static";
      if (checkboxField.Checked)
      {
        attributeDictionary["parallaxspeed"] = item.Fields[Templates.HasParallaxBackground.Fields.ParallaxSpeed].Value;
      }

      
      var attributes = attributeDictionary.Select(x => $"data-multibackground-layer-0-{x.Key}='{x.Value}'").ToList();
      attributes.Add("data-multibackground");
      return new HtmlString(string.Join(" ", attributes));
    }
  }
}