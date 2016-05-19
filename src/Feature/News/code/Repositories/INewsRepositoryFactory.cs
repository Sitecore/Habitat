namespace Sitecore.Feature.News.Repositories
{
  using Sitecore.Data.Items;

  public interface INewsRepositoryFactory
  {
    INewsRepository Create(Item contextItem);
  }
}