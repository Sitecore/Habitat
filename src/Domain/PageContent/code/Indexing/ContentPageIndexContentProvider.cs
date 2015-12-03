namespace Sitecore.Feature.PageContent.Indexing
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;
  using Sitecore.Foundation.Indexing.Infrastructure;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Web.UI.WebControls;

  public class PageContentIndexContentProvider : IndexContentProviderBase
  {
    public override string ContentType => DictionaryRepository.Get("/pagecontent/search/contenttype", "Page");

    public override IEnumerable<ID> SupportedTemplates => new[]
    {
      Templates.HasPageContent.ID
    };

    public override Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
    {
      var fieldNames = new[]
      {
        Templates.HasPageContent.Fields.Title_FieldName, Templates.HasPageContent.Fields.Summary_FieldName, Templates.HasPageContent.Fields.Body_FieldName
      };
      return this.GetFreeTextPredicate(fieldNames, query);
    }

    public override void FormatResult(SearchResultItem item, ISearchResult formattedResult)
    {
      var contentItem = item.GetItem();
      formattedResult.Title = FieldRenderer.Render(contentItem, Templates.HasPageContent.Fields.Title.ToString());
      formattedResult.Description = FieldRenderer.Render(contentItem, Templates.HasPageContent.Fields.Summary.ToString());
    }
  }
}