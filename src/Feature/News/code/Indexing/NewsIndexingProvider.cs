namespace Sitecore.Feature.News.Indexing
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

  public class NewsIndexingProvider : ProviderBase, ISearchResultFormatter, IQueryPredicateProvider
  {
    public string ContentType => DictionaryRepository.Get("/news/search/contenttype", "News");

    public IEnumerable<ID> SupportedTemplates => new[]
    {
      Templates.NewsArticle.ID
    };

    public Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
    {
      var fieldNames = new[]
      {
        Templates.NewsArticle.Fields.Title_FieldName, Templates.NewsArticle.Fields.Summary_FieldName, Templates.NewsArticle.Fields.Body_FieldName
      };
      return GetFreeTextPredicateService.GetFreeTextPredicate(fieldNames, query);
    }

    public void FormatResult(SearchResultItem item, ISearchResult formattedResult)
    {
      var contentItem = item.GetItem();
      formattedResult.Title = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.Title.ToString());
      formattedResult.Description = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.Summary.ToString());
      formattedResult.ViewName = "~/Views/News/NewsSearchResult.cshtml";
    }
  }
}