namespace Sitecore.Feature.Maps.Repositories
{
  using System.Collections.Generic;

  public interface IMapRepository
  {
    IEnumerable<Data.Items.Item> GetAll(Data.Items.Item contextItem);
  }
}