using System;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;

namespace Habitat.Framework.Indexing.Models
{
    internal class SearchResult : ISearchResult
    {
        private Uri _url;

        public SearchResult(Item item)
        {
            Item = item;
        }

        public Item Item { get; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }

        public Uri Url
        {
            get { return _url ?? new Uri(Item.Url(), UriKind.Relative); }
            set { _url = value; }
        }
    }
}