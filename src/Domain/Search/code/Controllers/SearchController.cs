namespace Habitat.Search.Controllers
{
  using System.Web.Mvc;
  using Habitat.Framework.Indexing.Models;
  using Habitat.Search.Models;

  public class SearchController : Controller
  {
    public ActionResult SearchResults(string query)
    {
      return this.View("SearchResults", this.GetSearchResults(query));
    }

    public ActionResult GlobalSearch()
    {
      return this.View("GlobalSearch", this.GetSearchSettings());
    }

    public ActionResult SearchSettings(string query)
    {
      return this.View("SearchSettings", this.GetSearchSettings(query));
    }

    private ISearchResults GetSearchResults(string queryText)
    {
      var results = this.HttpContext.Items["SearchResults"] as ISearchResults;
      if (results != null)
      {
        return results;
      }

      var query = this.CreateQuery(queryText);
      results = SearchServiceRepository.Get().Search(query);
      this.HttpContext.Items.Add("SearchResults", results);
      return results;
    }

    private IQuery CreateQuery(string queryText)
    {
      return QueryRepository.Get(queryText);
    }

    private SearchSettings GetSearchSettings(string query = null)
    {
      return SearchSettingsRepository.Get(query);
    }
  }
}