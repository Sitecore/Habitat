namespace Sitecore.Feature.Media.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using Sitecore.Data.Items;
  using Sitecore.Resources.Media;

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
          return this.MediaItem.MimeType.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }

        return string.Empty;
      }
    }

    public string MediaFormat => this.MediaItem != null ? this.MediaItem.MimeType.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries).Last() : string.Empty;

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
        if (!this.IsMedia)
        {
          return string.Empty;
        }

        var mediaUrl = string.Empty;
        var item = this.MediaItem;
        if (item != null)
        {
          mediaUrl = MediaManager.GetMediaUrl(item);
        }

        return $"style=background-image:url('{mediaUrl}');";
      }
    }

    public string MediaUrl => this.MediaItem != null ? MediaManager.GetMediaUrl(this.MediaItem) : string.Empty;

    public bool IsMedia => !string.IsNullOrEmpty(this.Type) && this.Type.Equals("bg-media");

    protected MediaItem MediaItem
    {
      get
      {
        if (this.mediaItem != null)
          return this.mediaItem;
        if (string.IsNullOrEmpty(this.Media))
          return this.mediaItem;

        var id = HttpUtility.UrlDecode(this.Media);
        var item = Context.Database.GetItem(id);
        this.mediaItem = new MediaItem(item);

        return this.mediaItem;
      }
    }
  }
}