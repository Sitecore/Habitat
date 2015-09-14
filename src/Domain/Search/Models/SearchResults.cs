using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace Habitat.Search.Models
{
    public class SearchResults
    {
        public string Query { get; set; }
        public Item ConfigurationItem { get; set; }
        public IEnumerable<SearchResult> Results { get; set; }
    }
}
