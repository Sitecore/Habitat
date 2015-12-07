namespace Sitecore.Feature.Search.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Feature.Search.Repositories;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public class SearchController : Controller
  {
    private readonly ISearchServiceRepository searchServiceRepository;
    private readonly ISearchSettingsRepository searchSettingsRepository;
    private readonly QueryRepository queryRepository;
    private readonly IRenderingPropertiesRepository renderingPropertiesRepository;

    public SearchController(): this(new SearchServiceRepository(), new SearchSettingsRepository(), new QueryRepository(), new RenderingPropertiesRepository())
    {
    }

    public SearchController(ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      this.searchServiceRepository = serviceRepository;
      this.queryRepository = queryRepository;
      this.searchSettingsRepository = settingsRepository;
      this.renderingPropertiesRepository = renderingPropertiesRepository;
    }

    [HttpGet]
    public ActionResult SearchResults(string query)
    {
      return this.View("SearchResults", this.GetSearchResults(new SearchQuery {Query = query}));
    }

    public ActionResult GlobalSearch()
    {
      return this.View("GlobalSearch", this.GetSearchSettings());
    }

    public ActionResult SearchSettings(string query)
    {
      return this.View("SearchSettings", this.GetSearchSettings(query));
    }

    public ActionResult PagedSearchResults(string query, int? page)
    {
      var pagingSettings =  this.renderingPropertiesRepository.Get<PagingSettings>();
      var pageNumber = page ?? 1;
      var results = this.GetSearchResults(new SearchQuery { Query = query, Page = pageNumber, ResultsOnPage = pagingSettings.ResultsOnPage });
      var pageble = new PagedSearchResults(pageNumber, results.TotalNumberOfResults, pagingSettings.PagesToShow, pagingSettings.ResultsOnPage);
      pageble.Query = query;
      pageble.Results = results;
      return this.View(pageble);
    }

    private ISearchResults GetSearchResults(SearchQuery searchQuery)
    {
      ISearchResults results = null;
      if (this.HttpContext != null)
      {
        results = this.HttpContext.Items["SearchResults"] as ISearchResults;
      }

      if (results != null)
      {
        return results;
      }

      var query = this.CreateQuery(searchQuery);
      results = this.searchServiceRepository.Get().Search(query);
      if (this.HttpContext != null)
      {
        this.HttpContext.Items.Add("SearchResults", results);
      }

      return results;
    }

    private IQuery CreateQuery(SearchQuery query)
    {
      return this.queryRepository.Get(query);
    }

    private SearchSettings GetSearchSettings(string query = null)
    {
      return this.searchSettingsRepository.Get(query);
    }
  }
}