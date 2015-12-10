namespace Sitecore.Foundation.Indexing.Models
{
  using System.Linq;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Infrastructure;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class SearchResultRepository
  {
    public static ISearchResult Create(SearchResultItem result)
    {
      var item = result.GetItem();
      var formattedResult = new SearchResult(item);
      FormatResultUsingFirstSupportedProvider(result, item, formattedResult);
      return formattedResult;
    }

    private static void FormatResultUsingFirstSupportedProvider(SearchResultItem result, Item item, ISearchResult formattedResult)
    {
      var formatter = FindFirstSupportedFormatter(item) ?? IndexContentProviderRepository.Default;
      formattedResult.ContentType = formatter.ContentType;
      formatter.FormatResult(result, formattedResult);
    }

    private static IndexContentProviderBase FindFirstSupportedFormatter(Item item)
    {
      var formatter = IndexContentProviderRepository.All.FirstOrDefault(provider => provider.SupportedTemplates.Any(item.IsDerived));
      return formatter;
    }
  }
}