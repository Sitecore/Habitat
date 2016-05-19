namespace Sitecore.Foundation.Indexing.Repositories
{
  using Sitecore.Foundation.Indexing;
  using Sitecore.Foundation.Indexing.Models;

  public class SearchServiceRepository : ISearchServiceRepository
  {
    private readonly ISearchSettings settings;

    public SearchServiceRepository() : this(new SearchSettingsBase())
    {
    }

    public SearchServiceRepository(ISearchSettings searchSettings)
    {
      this.settings = searchSettings;
    }

    public virtual SearchService Get()
    {
      return new SearchService(this.settings);
    }
  }
}