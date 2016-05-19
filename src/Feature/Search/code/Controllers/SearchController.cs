namespace Sitecore.Feature.Search.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Feature.Search.Repositories;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public class SearchController : Controller
  {
    public ISearchServiceRepository SearchServiceRepository { get; }
    public ISearchContextRepository SearchContextRepository { get; }
    public QueryRepository QueryRepository { get; }
    public IRenderingPropertiesRepository RenderingPropertiesRepository { get; }

    public SearchController() : this(new SearchContextRepository(), new QueryRepository(), new RenderingPropertiesRepository())
    {
    }
    public SearchController(ISearchContextRepository contextRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository) : this(new SearchServiceRepository(contextRepository.Get()), contextRepository, queryRepository, renderingPropertiesRepository)
    {
    }

    public SearchController(ISearchServiceRepository serviceRepository, ISearchContextRepository contextRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      this.SearchServiceRepository = serviceRepository;
      this.QueryRepository = queryRepository;
      this.SearchContextRepository = contextRepository;
      this.RenderingPropertiesRepository = renderingPropertiesRepository;
    }

    public ActionResult SearchResults(string query)
    {
      return this.View("SearchResults", this.GetSearchResults(new SearchQuery
                                                              {
                                                                Query = query
                                                              }));
    }

    public ActionResult GlobalSearch()
    {
      return this.View("GlobalSearch", this.GetSearchContext());
    }

    public ActionResult SearchResultsHeader(string query)
    {
      return this.View("SearchResultsHeader", this.GetSearchContext());
    }

    public ActionResult PagedSearchResults(string query, int? page)
    {
      var pagingSettings = this.RenderingPropertiesRepository.Get<PagingSettings>();
      var pageNumber = page ?? 1;
      var resultsOnPage = pagingSettings.ResultsOnPage <= 1 ? Models.PagedSearchResults.DefaultResultsOnPage : pagingSettings.ResultsOnPage;
      var results = this.GetSearchResults(new SearchQuery
                                          {
                                            Query = query,
                                            Page = pageNumber,
                                            ResultsOnPage = resultsOnPage
                                          });
      var pageble = new PagedSearchResults(pageNumber, results.TotalNumberOfResults, pagingSettings.PagesToShow, resultsOnPage)
                    {
                      Query = query,
                      Results = results
                    };
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
      results = this.SearchServiceRepository.Get().Search(query);
      this.HttpContext?.Items.Add("SearchResults", results);
      return results;
    }

    private IQuery CreateQuery(SearchQuery query)
    {
      return this.QueryRepository.Get(query);
    }

    private SearchContext GetSearchContext()
    {
      return this.SearchContextRepository.Get();
    }
  }
}