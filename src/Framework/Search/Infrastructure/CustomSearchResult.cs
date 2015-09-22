using System.Collections.Generic;
using System.ComponentModel;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Converters;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Links;

namespace Habitat.Framework.Indexing.Infrastructure
{
    public class CustomSearchResult : SearchResultItem
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }

        public override string Url
        {
            get { return LinkManager.GetItemUrl(GetItem()); }
        }

        [TypeConverter(typeof(IndexFieldEnumerableConverter))]
        [IndexField("__base_template")]
        public IEnumerable<ID> BaseTemplate { get; set; }
    }
}