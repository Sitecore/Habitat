namespace Sitecore.Feature.Search.Repositories
{
  using Sitecore.Foundation.Indexing;

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