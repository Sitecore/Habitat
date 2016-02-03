using Sitecore.Data.Items;

namespace Sitecore.Feature.News.Repositories
{
  public interface INewsRepositoryCreator
  {
    INewsRepository Create(Item contextItem);
  }
}