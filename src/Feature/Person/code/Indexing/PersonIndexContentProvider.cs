namespace Sitecore.Feature.Person.Indexing
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.Linq.Expressions;
  using Sitecore.Foundation.Indexing.Infrastructure;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;
  using Sitecore.Web.UI.WebControls;

  public class PersonIndexContentProvider : IndexContentProviderBase
  {
    public override string ContentType => DictionaryRepository.Get("/person/search/contenttype", "Person");

    public override IEnumerable<ID> SupportedTemplates => new[]
    {
      Templates.Employee.ID
    };

    //TODO: Unit test
    [ExcludeFromCodeCoverage]
    public override Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
    {
      var fieldNames = new[]
      {
        Templates.Person.Fields.Title_FieldName, Templates.Person.Fields.Summary_FieldName, Templates.Person.Fields.Name_FieldName, Templates.Employee.Fields.Biography_FieldName
      };
      return this.GetFreeTextPredicate(fieldNames, query);
    }

    //TODO: Unit test
    [ExcludeFromCodeCoverage]
    public override void FormatResult(SearchResultItem item, ISearchResult formattedResult)
    {
      var contentItem = item.GetItem();
      formattedResult.Title = FieldRenderer.Render(contentItem, Templates.Person.Fields.Name.ToString());
      formattedResult.Description = FieldRenderer.Render(contentItem, Templates.Person.Fields.Summary.ToString());
    }
  }
}