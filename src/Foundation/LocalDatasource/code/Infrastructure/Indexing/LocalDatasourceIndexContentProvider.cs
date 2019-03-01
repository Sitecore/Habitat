namespace Sitecore.Foundation.LocalDatasource.Infrastructure.Indexing
{
    using System;
    using System.Collections.Generic;
    using System.Configuration.Provider;
    using System.Linq.Expressions;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;
    using Sitecore.Foundation.Indexing.Infrastructure;
    using Sitecore.Foundation.Indexing.Models;

    public class LocalDatasourceQueryPredicateProvider : ProviderBase, IQueryPredicateProvider
  {
    public IEnumerable<ID> SupportedTemplates => new[]
    {
      TemplateIDs.StandardTemplate
    };

    public Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
    {
      var fieldNames = new[]
      {
        Templates.Index.Fields.LocalDatasourceContent_IndexFieldName
      };
      return GetFreeTextPredicateService.GetFreeTextPredicate(fieldNames, query);
    }
  }
}