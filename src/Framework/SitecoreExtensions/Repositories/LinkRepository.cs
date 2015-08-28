using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Habitat.Framework.SitecoreExtensions.Model;
using Habitat.Framework.SitecoreExtensions.Services;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Habitat.Framework.SitecoreExtensions.Repositories
{
  public class LinkRepository : XmlParserService
  {
    public static Link GetLinkFromXml(string xml)
    {
      Assert.IsNotNullOrEmpty(xml, "Xml is null");
      var xmlDocument = ValidateAndReturnXmlDocument(xml);
      return GetFromXml(xmlDocument.FirstChild);
    }

    private static Link GetFromXml(XmlNode node)
    {
      var target = GetAttribute(node, "target");
      var text = GetAttribute(node, "text");
      var title = GetAttribute(node, "title");
      var linkclass = GetAttribute(node, "class");
      var querystring = GetAttribute(node, "querystring");
      var url = GetUrl(node);
      var attributeValue6 = GetAttribute(node, "id");
      if (!string.IsNullOrEmpty(querystring))
        url += querystring.StartsWith("?") ? querystring : "?" + querystring;
      return new Link
      {
        Target = (string.IsNullOrEmpty(target) ? "_self" : target),
        Text = text,
        Title = (string.IsNullOrEmpty(title) ? text : title),
        Url = url,
        QueryString = (string.IsNullOrEmpty(querystring) ? null : ParseQueryString(querystring)),
        TargetId = (!string.IsNullOrEmpty(attributeValue6) ? attributeValue6 : null),
        CssClass = linkclass
      };
    }

    private static IEnumerable<KeyValuePair<string, string>> ParseQueryString(string queryString)
    {
      if (string.IsNullOrEmpty(queryString))
        return null;
      if (queryString[0] == 63 && queryString.Length > 1)
        queryString = queryString.Substring(1, queryString.Length - 1);
      return
        queryString.Split('&')
          .Select(queryPart => queryPart.Split('='))
          .Where(keyAndValue => keyAndValue.Length == 2)
          .Select(keyAndValue => new KeyValuePair<string, string>(keyAndValue[0], keyAndValue[1]));
    }

    private static string GetUrl(XmlNode node)
    {
      var linkType = GetAttribute(node, "linktype");
      var id = GetAttribute(node, "id");

      string str;
      if (linkType == "internal" || linkType == "media")
      {
        var itemFromIdNode = DatabaseRepository.GetActiveDatabase().GetItem(new ID(id));
        str = itemFromIdNode != null
          ? (linkType != "media"
            ? LinkManager.GetItemUrl(itemFromIdNode)
            : StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(new MediaItem(itemFromIdNode))))
          : "#BROKENLINK";
      }
      else
        str = GetAttribute(node, "url");
      return str;
    }
  }
}