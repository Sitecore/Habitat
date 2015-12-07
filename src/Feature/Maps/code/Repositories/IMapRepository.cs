namespace Habitat.Maps.Repositories
{
  using System.Collections.Generic;
  using Sitecore.Data.Items;

  public interface IMapRepository
  {
    IEnumerable<Item> GetAll(Item contextItem);
  }
}