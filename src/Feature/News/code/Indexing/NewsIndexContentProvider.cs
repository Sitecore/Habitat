namespace Sitecore.Feature.News.Indexing
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.Indexing.Infrastructure;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Web.UI.WebControls;

  public class NewsIndexContentProvider : IndexContentProviderBase
  {
    public override string ContentType => DictionaryPhraseRepository.Current.Get("/News/Search/Content Type", "News");

    public override IEnumerable<ID> SupportedTemplates => new[] {Templates.NewsArticle.ID};

    public override Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
    {
      var fieldNames = new[] {Templates.NewsArticle.Fields.Title_FieldName, Templates.NewsArticle.Fields.Summary_FieldName, Templates.NewsArticle.Fields.Body_FieldName};
      return this.GetFreeTextPredicate(fieldNames, query);
    }

    public override void FormatResult(SearchResultItem item, ISearchResult formattedResult)
    {
      var contentItem = item.GetItem();
      formattedResult.Title = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.Title.ToString());
      formattedResult.Description = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.Summary.ToString());
    }
  }
}