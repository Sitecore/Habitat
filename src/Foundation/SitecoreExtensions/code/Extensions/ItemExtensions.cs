namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.SitecoreExtensions.Services;
    using Sitecore.Links;
    using Sitecore.Resources.Media;

    public static class ItemExtensions
    {
        public static string Url(this Item item, UrlOptions options = null)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (options != null)
            {
                return LinkManager.GetItemUrl(item, options);
            }
            return !item.Paths.IsMediaItem ? LinkManager.GetItemUrl(item) : MediaManager.GetMediaUrl(item);
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

        public static string ImageUrl(this MediaItem mediaItem, int width, int height)
        {
            if (mediaItem == null)
            {
                throw new ArgumentNullException(nameof(mediaItem));
            }

            var options = new MediaUrlOptions {Height = height, Width = width};
            var url = MediaManager.GetMediaUrl(mediaItem, options);
            var cleanUrl = StringUtil.EnsurePrefix('/', url);
            var hashedUrl = HashingUtils.ProtectAssetUrl(cleanUrl);

            return hashedUrl;
        }


        public static Item TargetItem(this Item item, ID linkFieldId)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (item.Fields[linkFieldId] == null || !item.Fields[linkFieldId].HasValue)
            {
                return null;
            }
            return ((LinkField)item.Fields[linkFieldId]).TargetItem ?? ((ReferenceField)item.Fields[linkFieldId]).TargetItem;
        }

        public static string MediaUrl(this Item item, ID mediaFieldId, MediaUrlOptions options = null)
        {
            var targetItem = item.TargetItem(mediaFieldId);
            return targetItem == null ? string.Empty : (MediaManager.GetMediaUrl(targetItem) ?? string.Empty);
        }


        public static bool IsImage(this Item item)
        {
            return new MediaItem(item).MimeType.StartsWith("image/", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsVideo(this Item item)
        {
            return new MediaItem(item).MimeType.StartsWith("video/", StringComparison.InvariantCultureIgnoreCase);
        }

        public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateID)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return item.DescendsFrom(templateID) ? item : item.Axes.GetAncestors().LastOrDefault(i => i.DescendsFrom(templateID));
        }

        public static IList<Item> GetAncestorsAndSelfOfTemplate(this Item item, ID templateID)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var returnValue = new List<Item>();
            if (item.DescendsFrom(templateID))
            {
                returnValue.Add(item);
            }

            returnValue.AddRange(item.Axes.GetAncestors().Reverse().Where(i => i.DescendsFrom(templateID)));
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
            if (field == null || !(FieldTypeManager.GetField(field) is LinkField))
            {
                return string.Empty;
            }
            LinkField linkField = field;
            switch (linkField.LinkType.ToLower())
            {
                case "internal":
                    // Use LinkMananger for internal links, if link is not empty
                    return linkField.TargetItem != null ? LinkManager.GetItemUrl(linkField.TargetItem) : string.Empty;
                case "media":
                    // Use MediaManager for media links, if link is not empty
                    return linkField.TargetItem != null ? MediaManager.GetMediaUrl(linkField.TargetItem) : string.Empty;
                case "external":
                    // Just return external links
                    return linkField.Url;
                case "anchor":
                    // Prefix anchor link with # if link if not empty
                    return !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
                case "mailto":
                    // Just return mailto link
                    return linkField.Url;
                case "javascript":
                    // Just return javascript
                    return linkField.Url;
                default:
                    // Just please the compiler, this
                    // condition will never be met
                    return linkField.Url;
            }
        }

        public static string LinkFieldTarget(this Item item, ID fieldID)
        {
            return item.LinkFieldOptions(fieldID, LinkFieldOption.Target);
        }

        public static string LinkFieldOptions(this Item item, ID fieldID, LinkFieldOption option)
        {
            XmlField field = item.Fields[fieldID];
            switch (option)
            {
                case LinkFieldOption.Text:
                    return field?.GetAttribute("text");
                case LinkFieldOption.LinkType:
                    return field?.GetAttribute("linktype");
                case LinkFieldOption.Class:
                    return field?.GetAttribute("class");
                case LinkFieldOption.Alt:
                    return field?.GetAttribute("title");
                case LinkFieldOption.Target:
                    return field?.GetAttribute("target");
                case LinkFieldOption.QueryString:
                    return field?.GetAttribute("querystring");
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }

        public static bool HasLayout(this Item item)
        {
            return item?.Visualization?.Layout != null;
        }

        public static bool FieldHasValue(this Item item, ID fieldID)
        {
            return item.Fields[fieldID] != null && !string.IsNullOrWhiteSpace(item.Fields[fieldID].Value);
        }

        public static int? GetInteger(this Item item, ID fieldId)
        {
            int result;
            return !int.TryParse(item.Fields[fieldId].Value, out result) ? new int?() : result;
        }

        public static double? GetDouble(this Item item, ID fieldId)
        {
            var value = item?.Fields[fieldId]?.Value;
            if (value == null)
            {
                return null;
            }

            double num;
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num) || double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out num) || double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out num))
            {
                return num;
            }
            return null;
        }

        public static IEnumerable<Item> GetMultiListValueItems(this Item item, ID fieldId)
        {
            return new MultilistField(item.Fields[fieldId]).GetItems();
        }

        public static bool HasContextLanguage(this Item item)
        {
            var latestVersion = item.Versions.GetLatestVersion();
            return latestVersion?.Versions.Count > 0;
        }

        public static HtmlString Field(this Item item, ID fieldId)
        {
            Assert.IsNotNull(item, "Item cannot be null");
            Assert.IsNotNull(fieldId, "FieldId cannot be null");
            return new HtmlString(FieldRendererService.RenderField(item, fieldId));
        }
    }

    public enum LinkFieldOption
    {
        Text,
        LinkType,
        Class,
        Alt,
        Target,
        QueryString
    }
}