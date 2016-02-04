using Sitecore.Data.Items;

namespace Sitecore.Feature.News.Repositories
{
  public interface INewsRepositoryFactory
  {
    INewsRepository Create(Item contextItem);
  }
}