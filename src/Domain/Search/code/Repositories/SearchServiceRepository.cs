namespace Habitat.Search.Repositories
{
  using Habitat.Framework.Indexing;

  public class SearchServiceRepository : ISearchServiceRepository
  {
    private readonly ISearchSettingsRepository settingsRepository;
    public SearchServiceRepository(): this(new SearchSettingsRepository())
    {
    }

    public SearchServiceRepository(ISearchSettingsRepository settingsRepository)
    {
      this.settingsRepository = settingsRepository;
    }

    public virtual SearchService Get()
    {
      return new SearchService(this.settingsRepository.Get());
    }
  }
}