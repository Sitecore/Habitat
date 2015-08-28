using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Xml.Xsl;

namespace Habitat.Framework.SitecoreExtensions.Extensions
{
    /// <summary>
    /// Extension of Sitecore iems. A few common used fields of Sitecore Items. Make life slightly easier.
    /// </summary>
    public static class ItemExtensions
    {
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

            var imageField = (ImageField) item.Fields[imageFieldId];
            return imageField?.MediaItem == null ? string.Empty : imageField.ImageUrl(options);
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

            var mf = (MultilistField) item.Fields[fieldName];
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

            return item.Axes.GetAncestors().FirstOrDefault(i => i.IsDerived(templateID));
        }

        public static IList<Item> GetAncestorsAndSelfOfTemplate(this Item item, ID templateID)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            List<Item> returnValue = new List<Item>();
            if (item.IsDerived(templateID))
            {
                returnValue.Add(item);
            }

            returnValue.AddRange(item.Axes.GetAncestors().Where(i => i.IsDerived(templateID)));
            return returnValue;
        }

        public static string LinkFieldUrl(this Item item, ID fieldID)
        {
            var linkUrl = new LinkUrl();
            return linkUrl.GetUrl(item, fieldID.ToString());
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            if (item == null)
            {
                return false;
            }

            return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
        }

        public static bool IsDerived(this Item item, Item templateItem)
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
    }
}