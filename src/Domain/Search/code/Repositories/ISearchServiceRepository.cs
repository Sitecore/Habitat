namespace Sitecore.Feature.Search.Repositories
{
  using Sitecore.Foundation.Indexing;

  public interface ISearchServiceRepository
  {
    SearchService Get();
  }
}