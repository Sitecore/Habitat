namespace Sitecore.Feature.PageContent.Indexing
{
  using System;
  using System.Collections.Generic;
  using System.Configuration.Provider;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;
  using Sitecore.Foundation.Indexing.Infrastructure;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Web.UI.WebControls;

  public class PageContentIndexingProvider : ProviderBase, ISearchResultFormatter, IQueryPredicateProvider
  {
    public string ContentType => DictionaryRepository.Get("/pagecontent/search/contenttype", "Page");

    public IEnumerable<ID> SupportedTemplates => new[]
    {
      Templates.HasPageContent.ID
    };

    public Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
    {
      var fieldNames = new[]
      {
        Templates.HasPageContent.Fields.Title_FieldName, Templates.HasPageContent.Fields.Summary_FieldName, Templates.HasPageContent.Fields.Body_FieldName
      };
      return GetFreeTextPredicateService.GetFreeTextPredicate(fieldNames, query);
    }

    public void FormatResult(SearchResultItem item, ISearchResult formattedResult)
    {
      var contentItem = item.GetItem();
      formattedResult.Title = FieldRenderer.Render(contentItem, Templates.HasPageContent.Fields.Title.ToString());
      formattedResult.Description = FieldRenderer.Render(contentItem, Templates.HasPageContent.Fields.Summary.ToString());
    }
  }
}