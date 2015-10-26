using Habitat.Framework.Indexing;

namespace Habitat.Search.Models
{
    internal class SearchServiceRepository
    {
        public static SearchService Get()
        {
            return new SearchService(SearchSettingsRepository.Get());
        }
    }
}