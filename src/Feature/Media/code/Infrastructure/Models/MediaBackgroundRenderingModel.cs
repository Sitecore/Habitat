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
  using static System.String;

  public class MediaBackgroundRenderingModel
  {
    private MediaItem mediaItem;

    public string Type { get; set; }
    public string Media { get; set; }
    public string Parallax { get; set; }
    public string UseStaticPlaceholderNames { get; set; }

    public string MediaType
    {
      get
      {
        if (this.MediaItem != null)
        {
          return this.MediaItem.MimeType.Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }

        return Empty;
      }
    }

    public string MediaFormat
    {
      get
      {
        if (this.MediaItem != null)
        {
          return this.MediaItem.MimeType.Split(new [] {'/'}, StringSplitOptions.RemoveEmptyEntries).Last();  
        }

        return Empty;
      }
    }

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

        return Join(" ", classes);
      }
    }

    public string MediaAttribute
    {
      get
      {
        if (!this.IsMedia)
        {
          return Empty;
        }

        var mediaUrl = Empty;
        var item = this.MediaItem;
        if (item != null)
        {
          mediaUrl = MediaManager.GetMediaUrl(item);
        }

        return $"style=background-image:url('{mediaUrl}');";
      }
    }

    public string MediaUrl
    {
      get
      {
        if (this.MediaItem != null)
        {
          return MediaManager.GetMediaUrl(this.MediaItem);
        }

        return Empty;
      }
    }

    public bool IsMedia
    {
      get
      {
        if (!IsNullOrEmpty(this.Type) && this.Type.Equals("bg-media"))
        {
          return true;
        }

        return false;
      }
    }

    protected MediaItem MediaItem
    {
      get
      {
        if (this.mediaItem == null)
        {
          if (!IsNullOrEmpty(this.Media))
          {
            var id = HttpUtility.UrlDecode(this.Media);
            //var linkElement = XElement.Parse(linkValue);
            //var id = linkElement.Attribute("id")?.Value;
            var item = Context.Database.GetItem(id);
            this.mediaItem = new MediaItem(item);
          }
        }

        return this.mediaItem;
      }
    }
  }
}