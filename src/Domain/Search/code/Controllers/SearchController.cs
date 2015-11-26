namespace Habitat.Search.Controllers
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Mvc;
  using Habitat.Framework.Indexing;
  using Habitat.Framework.Indexing.Models;
  using Habitat.Search.Models;
  using Habitat.Search.Repositories;

  public class SearchController : Controller
  {
    private readonly ISearchServiceRepository searchServiceRepository;
    private readonly ISearchSettingsRepository searchSettingsRepository;
    private readonly QueryRepository queryRepository;

    public SearchController(): this(new SearchServiceRepository(), new SearchSettingsRepository(), new QueryRepository())
    {
    }

    public SearchController(ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository)
    {
      this.searchServiceRepository = serviceRepository;
      this.queryRepository = queryRepository;
      this.searchSettingsRepository = settingsRepository;
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
      var pageNumber = page ?? 1;
      var resultsonpage = 4;
      var results = this.GetSearchResults(new SearchQuery { Query = query, Page = pageNumber, ResultsOnPage = resultsonpage});
      var pageble = new PagedSearchResults(pageNumber, results.TotalNumberOfResults, 3, 4);
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