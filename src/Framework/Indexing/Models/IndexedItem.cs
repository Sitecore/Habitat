using System;
using System.Collections.Generic;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Habitat.Framework.Indexing.Models
{
    public class IndexedItem : SearchResultItem
    {
        [IndexField(Constants.IndexFields.HasPresentation)]
        public bool HasPresentation { get; set; }

        [IndexField(BuiltinFields.Semantics)]
        public string Tags { get; set; }

        [IndexField(Templates.IndexedItem.Fields.IncludeInSearchResults_FieldName)]
        public bool ShowInSearchResults { get; set; }

        [IndexField(Constants.IndexFields.AllTemplates)]
        public List<string> AllTemplates { get; set; }
    }
}