namespace Habitat.Search.Models
{
  using Habitat.Framework.Indexing;

  internal class SearchServiceRepository
  {
    public static SearchService Get()
    {
      return new SearchService(SearchSettingsRepository.Get());
    }
  }
}