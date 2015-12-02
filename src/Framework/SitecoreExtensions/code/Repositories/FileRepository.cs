namespace Sitecore.Framework.SitecoreExtensions.Repositories
{
  using System.Xml;
  using Sitecore.Data.Items;
  using Sitecore.Framework.SitecoreExtensions.Model;
  using Sitecore.Framework.SitecoreExtensions.Services;

  internal class FileRepository : XmlParserService
  {
    public static File Get(string xml)
    {
      var xmlDocument = ValidateAndReturnXmlDocument(xml);
      return GetFromXml(xmlDocument.FirstChild);
    }

    private static File GetFromXml(XmlNode node)
    {
      var mediaId = GetAttribute(node, "mediaid");
      var src = GetAttribute(node, "src");
      var file = new File
      {
        MediaId = mediaId,
        Source = src
      };
      if (string.IsNullOrEmpty(file.MediaId))
      {
        return file;
      }
      var mediaItemFromMediaId = DatabaseRepository.GetActiveDatabase().GetItem(file.MediaId);

      if (mediaItemFromMediaId == null)
      {
        return file;
      }

      var mediaItem = new MediaItem(mediaItemFromMediaId);
      file.Title = string.IsNullOrEmpty(mediaItem.Title)
        ? mediaItem.DisplayName
        : mediaItem.Title;
      file.Extension = mediaItem.Extension;
      file.FileSize = mediaItem.Size;
      return file;
    }
  }
}