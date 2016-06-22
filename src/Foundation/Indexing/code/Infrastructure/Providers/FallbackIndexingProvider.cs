namespace Sitecore.Foundation.Indexing.Infrastructure.Providers
{
  using System.Collections.Generic;
  using System.Configuration.Provider;
  using Sitecore;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data;
  using Sitecore.Foundation.Indexing.Models;

  public class FallbackSearchResultFormatter : ProviderBase, ISearchResultFormatter
  {
    public string ContentType => "[Unknown]";

    public IEnumerable<ID> SupportedTemplates => new[]
    {
      TemplateIDs.StandardTemplate
    };

    public void FormatResult(SearchResultItem item, ISearchResult formattedResult)
    {
      formattedResult.Title = $"[{item.Name}]";
      formattedResult.Description = $"[This item is indexed but has no content provider: {item.Path}]";
    }
  }
}