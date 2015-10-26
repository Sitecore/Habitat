using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using Habitat.Framework.Indexing.Models;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace Habitat.Framework.Indexing.Infrastructure
{
    public abstract class IndexContentProviderBase : ProviderBase
    {
        public abstract string ContentType { get; }
        public abstract IEnumerable<ID> SupportedTemplates { get; }
        public abstract Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query);
        public abstract void FormatResult(SearchResultItem item, ISearchResult formattedResult);

        protected Expression<Func<SearchResultItem, bool>> GetFreeTextPredicate(string[] fieldNames, IQuery query)
        {
            var predicate = PredicateBuilder.False<SearchResultItem>();
            return fieldNames.Aggregate(predicate, (current, fieldName) => current.Or(i => i[fieldName].Contains(query.QueryText)));
        }
    }
}
