using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Habitat.Framework.Indexing;
using Habitat.Framework.Indexing.Infrastructure;
using Habitat.Framework.Indexing.Models;
using Habitat.Framework.SitecoreExtensions.Repositories;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Web.UI.WebControls;
using ISearchResult = Habitat.Framework.Indexing.Models.ISearchResult;

namespace Habitat.News.Indexing
{
    public class NewsIndexContentProvider : IndexContentProviderBase
    {
        public override string ContentType => DictionaryRepository.Get("/news/search/contenttype", "News");
        public override IEnumerable<ID> SupportedTemplates => new[] {Templates.NewsArticle.ID};
        public override Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
        {
            var fieldNames = new[] {Templates.NewsArticle.Fields.Title_FieldName, Templates.NewsArticle.Fields.Summary_FieldName, Templates.NewsArticle.Fields.Body_FieldName};
            return GetFreeTextPredicate(fieldNames, query);
        }

        public override void FormatResult(SearchResultItem item, ISearchResult formattedResult)
        {
            var contentItem = item.GetItem();
            formattedResult.Title = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.Title.ToString());
            formattedResult.Description = FieldRenderer.Render(contentItem, Templates.NewsArticle.Fields.Summary.ToString());
        }
    }
}
