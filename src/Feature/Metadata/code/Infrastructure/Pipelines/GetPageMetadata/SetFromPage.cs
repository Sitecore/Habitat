namespace Sitecore.Feature.Metadata.Infrastructure.Pipelines.GetPageMetadata
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Metadata.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    [Service]
    public class SetFromPage
    {
        public void Process(GetPageMetadataArgs args)
        {
            if (!args.Item.IsDerived(Templates.PageMetadata.ID))
                return;

            this.SetKeywords(args.Item, args.Metadata.KeywordsList);
            this.SetPageTitle(args.Item, args.Metadata);
            this.SetDescription(args.Item, args.Metadata);
            this.SetIndexingFlags(args.Item, args.Metadata.Robots);
            this.SetCustomMetadata(args.Item, args.Metadata.CustomMetadata);
        }

        private void SetCustomMetadata(Item item, ICollection<KeyValuePair<string, string>> customMetadata)
        {
            if (!item.FieldHasValue(Templates.PageMetadata.Fields.CustomMetaData))
                return;
            var values = ((NameValueListField)item.Fields[Templates.PageMetadata.Fields.CustomMetaData])?.NameValues;
            if (values == null)
                return;
            foreach (var key in values.AllKeys)
            {
                var value = HttpUtility.UrlDecode(values[key]);
                customMetadata.Add(new KeyValuePair<string, string>(key, value));
            }
        }

        private void SetDescription(Item item, IMetadata metadata)
        {
            var description = item[Templates.PageMetadata.Fields.MetaDescription];
            if (string.IsNullOrWhiteSpace(description))
                return;
            metadata.Description = description;
        }

        private void SetPageTitle(Item item, IMetadata metadata)
        {
            var title = item[Templates.PageMetadata.Fields.BrowserTitle];
            if (string.IsNullOrWhiteSpace(title))
                return;
            metadata.PageTitle = title;
        }

        private void SetIndexingFlags(Item item, ICollection<string> robotsMetadata)
        {
            if (!(item.Fields[Templates.PageMetadata.Fields.CanIndex]?.IsChecked() ?? true))
            {
                robotsMetadata.Add("NOINDEX");
            }
            if (!(item.Fields[Templates.PageMetadata.Fields.SeoFollowLinks]?.IsChecked() ?? true))
            {
                robotsMetadata.Add("NOFOLLOW");
            }
        }

        private void SetKeywords(Item item, ICollection<string> keywordsList)
        {
            var keywordsField = item.Fields[Templates.PageMetadata.Fields.MetaKeywords];
            if (keywordsField == null)
                return;
            
            var keywordMultilist = new MultilistField(keywordsField);
            foreach (var keyword in keywordMultilist.GetItems().Select(i => i[Templates.Keyword.Fields.Keyword]))
            {
                keywordsList.Add(keyword);
            }
        }
    }
}