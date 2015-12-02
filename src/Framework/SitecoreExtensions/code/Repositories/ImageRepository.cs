namespace Sitecore.Framework.SitecoreExtensions.Repositories
{
  using System;
  using System.Xml;
  using Sitecore.Data.Items;
  using Sitecore.Framework.SitecoreExtensions.Model;
  using Sitecore.Framework.SitecoreExtensions.Services;

  internal class ImageRepository : XmlParserService
  {
    public static Image Get(string xml)
    {
      var xmlDocument = ValidateAndReturnXmlDocument(xml);
      if (xmlDocument == null)
      {
        throw new Exception("Xml document has invalid value or is null : " + xml);
      }
      return GetFromXml(xmlDocument.FirstChild);
    }

    private static Image GetFromXml(XmlNode node)
    {
      var mediaId = GetAttribute(node, "mediaid");
      var src = GetAttribute(node, "src");
      var mediaPath = GetAttribute(node, "mediapath");
      var alt = GetAttribute(node, "alt");
      var width = GetAttribute(node, "width");
      var height = GetAttribute(node, "height");
      var vspace = GetAttribute(node, "vspace");
      var hspace = GetAttribute(node, "hspace");

      var image = new Image
      {
        MediaId = mediaId,
        Source = src,
        MediaPath = mediaPath,
        AlternateText = alt
      };

      if (!string.IsNullOrEmpty(width))
      {
        image.Width = int.Parse(width);
      }
      if (!string.IsNullOrEmpty(height))
      {
        image.Height = int.Parse(height);
      }
      if (!string.IsNullOrEmpty(vspace))
      {
        image.VerticalSpace = int.Parse(vspace);
      }
      if (!string.IsNullOrEmpty(hspace))
      {
        image.HorisontalSpace = int.Parse(hspace);
      }

      if (string.IsNullOrEmpty(image.MediaId))
      {
        return image;
      }

      var mediaIdItem = DatabaseRepository.GetActiveDatabase().GetItem(image.MediaId);
      if (mediaIdItem == null)
      {
        return image;
      }

      var mediaItem = new MediaItem(mediaIdItem);
      image.Title = string.IsNullOrEmpty(mediaItem.Title)
        ? mediaItem.DisplayName
        : mediaItem.Title;
      image.Extension = mediaItem.Extension;
      image.FileSize = mediaItem.Size;
      if (string.IsNullOrEmpty(image.AlternateText))
      {
        image.AlternateText = mediaItem.Alt;
      }
      return image;
    }
  }
}