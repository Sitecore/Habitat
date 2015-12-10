namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Web;
  using Sitecore.Foundation.SitecoreExtensions.Model;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.Links;
  using Sitecore.Resources.Media;
  using Sitecore.Sites;
  using Sitecore.Xml.Xsl;

  /// <summary>
  ///   Extension of Sitecore iems. A few common used fields of Sitecore Items. Make life slightly easier.
  /// </summary>
  public static class ItemExtensions
  {

    public static string DisplayName(this ID itemId)
    {
      return DatabaseRepository.GetActiveDatabase().GetItem(itemId)?.DisplayName;
    }

    public static string DisplayName(this Guid itemId)
    {
      return DisplayName(new ID(itemId));
    }

    public static string Url(this Item item, UrlOptions options = null)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      return options != null ? LinkManager.GetItemUrl(item, options) : LinkManager.GetItemUrl(item);
    }

    public static string ImageUrl(this Item item, ID imageFieldId, MediaUrlOptions options = null)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      var imageField = (ImageField)item.Fields[imageFieldId];
      return imageField?.MediaItem == null ? string.Empty : imageField.ImageUrl(options);
    }

    public static string MediaUrl(this Item item, ID mediaFieldId, MediaUrlOptions options = null)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      var imageField = (LinkField)item.Fields[mediaFieldId];
      return imageField.TargetItem == null ? String.Empty : (MediaManager.GetMediaUrl(imageField.TargetItem) ?? string.Empty);
    }

    public static Item[] TargetItems(this Item item, string fieldName)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      if (fieldName == null)
      {
        throw new ArgumentNullException(nameof(fieldName));
      }

      var mf = (MultilistField)item.Fields[fieldName];
      return mf == null ? new Item[0] : mf.GetItems();
    }

    public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateID)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      if (item.IsDerived(templateID))
      {
        return item;
      }

      return item.Axes.GetAncestors().Reverse().FirstOrDefault(i => i.IsDerived(templateID));
    }

    public static IList<Item> GetAncestorsAndSelfOfTemplate(this Item item, ID templateID)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      var returnValue = new List<Item>();
      if (item.IsDerived(templateID))
      {
        returnValue.Add(item);
      }

      returnValue.AddRange(item.Axes.GetAncestors().Where(i => i.IsDerived(templateID)));
      return returnValue;
    }

    public static string LinkFieldUrl(this Item item, ID fieldID)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      if (ID.IsNullOrEmpty(fieldID))
      {
        throw new ArgumentNullException(nameof(fieldID));
      }
      var field = item.Fields[fieldID];
      if (field == null)
      {
        return string.Empty;
      }
      var linkUrl = new LinkUrl();
      return linkUrl.GetUrl(item, fieldID.ToString());
    }

    public static string LinkFieldTarget(this Item item, ID fieldID)
    {
      XmlField field = item.Fields[fieldID];
      return field?.GetAttribute("target");
    }

    public static bool IsDerived(this Item item, ID templateId)
    {
      if (item == null)
      {
        return false;
      }

      return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
    }

    private static bool IsDerived(this Item item, Item templateItem)
    {
      if (item == null)
      {
        return false;
      }

      if (templateItem == null)
      {
        return false;
      }

      var itemTemplate = TemplateManager.GetTemplate(item);
      return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
    }

    public static bool FieldHasValue(this Item item, ID fieldID)
    {
      return item.Fields[fieldID] != null && item.Fields[fieldID].HasValue && !string.IsNullOrWhiteSpace(item.Fields[fieldID].Value);
    }

    public static string GetString(this Item item, string fieldName)
    {
      return item.Fields[fieldName].Value;
    }

    public static string GetString(this Item item, ID fieldID)
    {
      return item.Fields[fieldID].Value;
    }

    public static void SetString(this Item item, ID fieldId, string value)
    {
      item.Fields[fieldId].Value = value;
    }

    public static bool GetCheckBoxValue(this Item item, ID fieldId)
    {
      return new CheckboxField(item.Fields[fieldId]).Checked;
    }

    public static void SetCheckBoxValue(this Item item, ID fieldId, bool value)
    {
      new CheckboxField(item.Fields[fieldId]).Checked = value;
    }

    public static DateTime? GetDate(this Item item, ID fieldId)
    {
      return new DateField(item.Fields[fieldId]).DateTime;
    }

    public static void SetDate(this Item item, ID fieldId, DateTime value)
    {
      new DateField(item.Fields[fieldId]).Value = DateUtil.ToIsoDate(value);
    }

    public static int? GetInteger(this Item item, ID fieldId)
    {
      int result;
      return !int.TryParse(item.Fields[fieldId].Value, out result) ? new int?() : result;
    }

    public static void SetInteger(this Item item, ID fieldId, int value)
    {
      item.Fields[fieldId].Value = value.ToString(CultureInfo.InvariantCulture);
    }

    public static decimal? GetDecimal(this Item item, ID fieldId)
    {
      decimal result;
      return !decimal.TryParse(item.Fields[fieldId].Value, out result) ? new decimal?() : result;
    }

    public static void SetDecimal(this Item item, ID fieldId, decimal value)
    {
      item.Fields[fieldId].Value = value.ToString(CultureInfo.InvariantCulture);
    }

    public static Item GetDropLinkSelectedItem(this Item item, ID fieldId)
    {
      return new InternalLinkField(item.Fields[fieldId]).TargetItem;
    }

    public static void SetDropLink(this Item item, ID fieldId, Item linkedItem)
    {
      Assert.ArgumentNotNull(linkedItem, "linkedItem");
      new InternalLinkField(item.Fields[fieldId]).Value = linkedItem.ID.Guid.ToString("P").ToUpper();
    }

    public static IEnumerable<Item> GetMultiListValues(this Item item, ID fieldId)
    {
      return new MultilistField(item.Fields[fieldId]).GetItems();
    }

    public static void SetMultiListValues(this Item item, ID fieldId, IEnumerable<Item> items)
    {
      var str = string.Join("|", items.Select(i => i.ID).Select(id => id.Guid.ToString("P").ToUpper()).ToArray());
      new MultilistField(item.Fields[fieldId]).Value = str;
    }

    public static Link GetLink(this Item item, ID fieldId)
    {
      return LinkRepository.GetLinkFromXml(item.Fields[fieldId].Value);
    }

    public static File GetFile(this Item item, ID fieldId)
    {
      return FileRepository.Get(item.Fields[fieldId].Value);
    }

    public static Image GetImage(this Item item, ID fieldId)
    {
      return ImageRepository.Get(item.Fields[fieldId].Value);
    }

    public static bool IsMedia(this Item item)
    {
      return item.Paths.IsMediaItem;
    }

    public static IEnumerable<Item> GetChildrenDerivedFrom(this Item item, TemplateItem template)
    {
      return item.GetChildrenDerivedFrom(template.ID);
    }

    public static IEnumerable<Item> GetChildrenDerivedFrom(this Item item, ID templateId)
    {
      return item.GetChildren().Where(c => IsDerived(c, templateId));
    }

    public static bool HasLanguage(this Item item, string languageName)
    {
      return ItemManager.GetVersions(item, LanguageManager.GetLanguage(languageName, item.Database)).Count > 0;
    }

    public static bool HasLanguage(this Item item, Language language)
    {
      return ItemManager.GetVersions(item, language).Count > 0;
    }

    public static bool HasContextLanguage(this Item item)
    {
      var latestVersion = item.Versions.GetLatestVersion();
      if (latestVersion != null)
      {
        return latestVersion.Versions.Count > 0;
      }
      return false;
    }

    public static string GetUrl(this Item item)
    {
      return !IsMedia(item) ? LinkManager.GetItemUrl(item) : MediaManager.GetMediaUrl(item);
    }

    public static bool IsInContextSite(this Item item)
    {
      Assert.IsNotNull(Context.Site, "Item is null");
      return IsInSite(item, Context.Site);
    }

    public static bool IsInSite(this Item item, SiteContext siteContext)
    {
      Assert.ArgumentNotNull(siteContext, "siteContext");
      Assert.IsNotNull(item, "Item is null");
      var rootItem = siteContext.Database.GetItem(siteContext.RootPath);
      for (var sitecoreItem = item; sitecoreItem != null; sitecoreItem = sitecoreItem.Parent)
      {
        if (sitecoreItem.ID.Guid.Equals(rootItem.ID.Guid))
        {
          return true;
        }
      }
      return false;
    }

    public static bool IsInDatabase(this Item item, Database database)
    {
      return database.GetItem(item.ID, item.Language, item.Version) != null;
    }

    public static Item[] GetReferrersAsItems(this Item item)
    {
      return Globals.LinkDatabase.GetReferrers(item).Select(i => i.GetSourceItem()).Where(i => i != null).ToArray();
    }

    public static Item[] GetReferencesAsItems(this Item item)
    {
      return Globals.LinkDatabase.GetReferences(item).Select(i => i.GetTargetItem()).Where(i => i != null).ToArray();
    }

    public static bool HasVersionedRenderings(this Item item)
    {
      if (item.Fields[FieldIDs.FinalLayoutField] == null)
      {
        return false;
      }
      var field = item.Fields[FieldIDs.FinalLayoutField];
      return !string.IsNullOrEmpty(field.GetValue(false, false));
    }

    public static bool HasVersionedRenderingsOnLanguage(this Item item, Language language)
    {
      return item != null && item.Database.GetItem(item.ID, language).HasVersionedRenderings();
    }

    public static bool HasVersionedRenderingsOnAnyLanguage(this Item item)
    {
      return ItemManager.GetContentLanguages(item).Any(item.HasVersionedRenderingsOnLanguage);
    }

    public static bool HasVersionedRenderingsOnContextLanguage(this Item item)
    {
      return item.HasVersionedRenderingsOnLanguage(Context.Language);
    }

    public static bool HasVersionedRenderingsOnVersion(this Item item, Language language, Sitecore.Data.Version version)
    {
      var versionItem = item.Database.GetItem(item.ID, language, version);
      return versionItem != null && versionItem.HasVersionedRenderings();
    }

    public static HtmlString Field(this Item item, ID fieldId)
    {
      Assert.IsNotNull(item, "Item cannot be null");
      Assert.IsNotNull(fieldId, "FieldId cannot be null");
      return new HtmlString(FieldRendererService.RenderField(item, fieldId));
    }

    public static HtmlString Field(this Item item, ID fieldId, object parameters)
    {
      return new HtmlString(FieldRendererService.BeginField(fieldId, item, parameters) + FieldRendererService.EndField().ToString());
    }
  }
}