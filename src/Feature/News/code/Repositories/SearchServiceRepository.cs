namespace Sitecore.Feature.News.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using Sitecore.Foundation.Indexing;

  public class SearchServiceRepository : ISearchServiceRepository
  {
    private readonly SearchSettingsRepository settingsRepository;

    public SearchServiceRepository() : this(new SearchSettingsRepository())
    {
    }

    public SearchServiceRepository(SearchSettingsRepository searchSettingsRepository)
    {
      this.settingsRepository = searchSettingsRepository;
    }

    public SearchService Get()
    {
      return new SearchService(this.settingsRepository.Get());
    }
  }
}