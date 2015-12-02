namespace Sitecore.Feature.Search.Text
{
  using Sitecore.Framework.SitecoreExtensions.Repositories;

  public static class Captions
  {
    public static string NoResults => DictionaryRepository.Get("/Search/Captions/NoResults", "No results found");
  }
}