namespace Sitecore.Foundation.Indexing.Repositories
{
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Foundation.Indexing;

  public class SearchServiceRepository : ISearchServiceRepository
  {
    private readonly ISearchSettingsRepository settingsRepository;

    public SearchServiceRepository() : this(new SearchSettingsRepositoryBase())
    {
    }

    public SearchServiceRepository(ISearchSettingsRepository searchSettingsRepository)
    {
      this.settingsRepository = searchSettingsRepository;
    }

    public virtual SearchService Get()
    {
      return new SearchService(this.settingsRepository.Get());
    }
  }
}