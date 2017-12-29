namespace Sitecore.Feature.News.Indexing
{
    using System;
    using System.Collections.Generic;
    using System.Configuration.Provider;
    using System.Linq.Expressions;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Foundation.Indexing.Infrastructure;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Web.UI.WebControls;

    public class NewsIndexingProvider : ProviderBase, ISearchResultFormatter, IQueryPredicateProvider
    {
        public Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
        {
            var fieldNames = new[] {Templates.NewsArticle.Fields.NewsTitle_FieldName, Templates.NewsArticle.Fields.NewsSummary_FieldName, Templates.NewsArticle.Fields.NewsBody_FieldName};
            return GetFreeTextPredicateService.GetFreeTextPredicate(fieldNames, query);
        }

        public string ContentType => DictionaryPhraseRepository.Current.Get("/News/Search/Content Type", "News");

        public IEnumerable<ID> SupportedTemplates => new[] {Templates.NewsArticle.ID};

        public void FormatResult(SearchResultItem item, ISearchResult formattedResult)
        {
            var contentItem = item.GetItem();
            if (contentItem == null)
            {
                return;
            }

            formattedResult.Title = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.NewsTitle.ToString());
            formattedResult.Description = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.NewsSummary.ToString());
            formattedResult.Media = ((ImageField)contentItem.Fields[Templates.NewsArticle.Fields.NewsImage])?.MediaItem;
            formattedResult.ViewName = "~/Views/News/NewsSearchResult.cshtml";
        }
    }
}