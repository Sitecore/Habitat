namespace Sitecore.Feature.Search.Repositories
{
  using Sitecore.Feature.Search.Models;
  using Sitecore.Foundation.Indexing.Models;

  public class QueryRepository
  {
    public IQuery Get(SearchQuery query)
    {
      return new Query
      {
        QueryText = query.Query,
        IndexOfFirstResult = query.Page == 0 ? 0 : (query.Page - 1) * query.ResultsOnPage,
        NoOfResults = query.ResultsOnPage == 0 ? 0 : query.ResultsOnPage
      };
    }
  }

  public class Query : IQuery
  {
    public string QueryText { get; set; }
    public int IndexOfFirstResult { get; set; }
    public int NoOfResults { get; set; }
  }
}