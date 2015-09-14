using System;
using System.Collections.Generic;
using Habitat.Framework.Taxonomy.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Habitat.Search.Models
{
    public class SearchResult : SearchResultItem
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string ContentType { get; set; }

        [IndexField(BuiltinFields.Semantics)]
        public string Tags { get; set; }
    }
}