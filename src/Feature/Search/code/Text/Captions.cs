namespace Sitecore.Feature.Search.Text
{
  using Sitecore.Foundation.Dictionary.Repositories;

  public static class Captions
  {
    public static string NoResults => DictionaryRepository.Get("/Search/Captions/NoResults", "No results found");
  }
}