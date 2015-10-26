using System.Web;
using Habitat.Framework.Indexing.Models;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;

namespace Habitat.Search.Models
{
    public class SearchSettings : ISearchSettings
    {
        public Item ConfigurationItem { get; set; }

        public string Query { get; set; }

        public string SearchBoxTitle { get;set; }

        public string SearchResultsUrl { get; set; }
        public Item Root { get; set; }
    }
}