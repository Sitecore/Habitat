namespace Sitecore.Feature.Maps.Repositories
{
  using System.Collections.Generic;
  using Models;

  public interface IMapPointRepository
  {
    IEnumerable<MapPoint> GetAll(Data.Items.Item contextItem);
  }
}