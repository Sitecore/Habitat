namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections.Generic;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Data;

    public interface ISearchResultFormatter
    {
        string ContentType { get; }
        IEnumerable<ID> SupportedTemplates { get; }
        void FormatResult(SearchResultItem item, ISearchResult formattedResult);
    }
}