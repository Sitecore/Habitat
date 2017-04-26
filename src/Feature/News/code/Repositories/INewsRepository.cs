namespace Sitecore.Feature.News.Repositories
{
  using System.Collections.Generic;
  using Sitecore.Data.Items;

  public interface INewsRepository
  {
    IEnumerable<Item> Get(Item contextItem);
    IEnumerable<Item> GetLatest(Item contextItem, int count);
  }
}