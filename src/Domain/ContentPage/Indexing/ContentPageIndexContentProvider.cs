using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Habitat.Framework.Indexing;
using Habitat.Framework.Indexing.Infrastructure;
using Habitat.Framework.Indexing.Models;
using Habitat.Framework.SitecoreExtensions.Repositories;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Web.UI.WebControls;
using ISearchResult = Habitat.Framework.Indexing.Models.ISearchResult;

namespace Habitat.ContentPage.Indexing
{
    public class ContentPageIndexContentProvider : IndexContentProviderBase
    {
        public override string ContentType => DictionaryRepository.Get("/pagecontent/search/contenttype", "Page");
        public override IEnumerable<ID> SupportedTemplates => new[] {Templates.HasPageContent.ID};
        public override Expression<Func<SearchResultItem, bool>> GetQueryPredicate(IQuery query)
        {
            var fieldNames = new[] {Templates.HasPageContent.Fields.Title_FieldName, Templates.HasPageContent.Fields.Summary_FieldName, Templates.HasPageContent.Fields.Body_FieldName};
            return GetFreeTextPredicate(fieldNames, query);
        }

        public override void FormatResult(SearchResultItem item, ISearchResult formattedResult)
        {
            var contentItem = item.GetItem();
            formattedResult.Title = FieldRenderer.Render(contentItem, Templates.HasPageContent.Fields.Title.ToString());
            formattedResult.Description = FieldRenderer.Render(contentItem, Templates.HasPageContent.Fields.Summary.ToString());
        }
    }
}
