namespace Sitecore.Feature.Media.Infrastructure.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Web;
  using System.Xml.Linq;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Resources.Media;
  using Sitecore.Web;

  public class MediaBackgroundRenderingModel
  {
    public string Type { get; set; }
    public string Media { get; set; }
    public string Parallax { get; set; }
    public string MediaType { get; set; }
    public string MediaFormat { get; set; }
    public string UseStaticPlaceholderNames { get; set; }

    public string CssClass
    {
      get
      {
        var classes = new List<string>
                      {
                        this.Type
                      };
        if (MainUtil.GetBool(this.Parallax, false))
        {
          classes.Add("bg-parallax");
        }

        return string.Join(" ", classes);
      }
    }

    public string MediaAttribute
    {
      get
      {
        if (this.Type == null || !this.Type.Equals("bg-media"))
        {
          return String.Empty;
        }

        var mediaUrl = string.Empty;
        if (!string.IsNullOrEmpty(this.Media))
        {
          var linkValue = HttpUtility.UrlDecode(this.Media);
          var linkElement = XElement.Parse(linkValue);
          var id = linkElement.Attribute("id")?.Value;
          var item = Context.Database.GetItem(id);

          if (item != null)
          {
            var mediaItem = new MediaItem(item);
            mediaUrl = MediaManager.GetMediaUrl(mediaItem);
          }
        }

        return $"style=background-image:url('{mediaUrl}');";
      }
    }

  }
}