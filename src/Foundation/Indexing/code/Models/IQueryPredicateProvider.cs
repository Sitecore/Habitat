namespace Sitecore.Foundation.Indexing.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;

  public interface IQueryPredicateProvider
  {
    Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query);
    IEnumerable<ID> SupportedTemplates { get; }
  }
}