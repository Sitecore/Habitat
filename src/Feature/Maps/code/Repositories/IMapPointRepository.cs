namespace Sitecore.Feature.Maps.Repositories
{
  using System.Collections.Generic;

  public interface IMapPointRepository
  {
    IEnumerable<Data.Items.Item> GetAll(Data.Items.Item contextItem);
  }
}