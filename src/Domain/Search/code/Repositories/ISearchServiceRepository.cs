namespace Sitecore.Feature.Search.Repositories
{
  using Sitecore.Framework.Indexing;

  public interface ISearchServiceRepository
  {
    SearchService Get();
  }
}