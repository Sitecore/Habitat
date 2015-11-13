namespace Habitat.Search.Models
{
  using Habitat.Framework.Indexing.Models;

  public static class QueryRepository
  {
    public static IQuery Get(string queryText)
    {
      return new Query
      {
        QueryText = queryText
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