namespace Sitecore.Framework.Indexing.Infrastructure
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using Sitecore;
  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;
  using Sitecore.Framework.Indexing.Models;

  public class FallbackIndexContentProvider : IndexContentProviderBase
  {
    public override string ContentType => "[Unknown]";

    public override IEnumerable<ID> SupportedTemplates => new[]
    {
      TemplateIDs.StandardTemplate
    };

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