namespace Sitecore.Feature.Search.Repositories
{
  using Sitecore.Feature.Search.Models;

  public interface ISearchContextRepository
  {
    SearchContext Get();
    SearchContext Get(string query);
  }
}