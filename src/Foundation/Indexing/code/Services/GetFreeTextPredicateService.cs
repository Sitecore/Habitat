namespace Sitecore.Foundation.Indexing.Infrastructure
{
  using System;
  using System.Linq;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Foundation.Indexing.Models;

  public static class GetFreeTextPredicateService
  {
    public static Expression<Func<SearchResultItem, bool>> GetFreeTextPredicate(string[] fieldNames, IQuery query)
    {
      var predicate = PredicateBuilder.False<SearchResultItem>();
      if (string.IsNullOrWhiteSpace(query.QueryText))
      {
        return predicate;
      }
      return fieldNames.Aggregate(predicate, (current, fieldName) => current.Or(i => i[fieldName].Contains(query.QueryText)));
    }
  }
}