using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Habitat.Framework.Indexing.Models;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace Habitat.Framework.Indexing.Infrastructure
{
    public class FallbackIndexContentProvider : IndexContentProviderBase
    {
        public override string ContentType => "[Unknown]";
        public override IEnumerable<ID> SupportedTemplates => new[] {Sitecore.TemplateIDs.StandardTemplate};
        public override Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
        {
            return PredicateBuilder.False<SearchResultItem>();
        }
        public override void FormatResult(SearchResultItem item, ISearchResult formattedResult)
        {
            formattedResult.Title = $"[{item.Name}]";
            formattedResult.Description = $"[This item is indexed but has no content provider: {item.Path}]";
        }
    }
}
