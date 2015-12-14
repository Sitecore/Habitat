
namespace Sitecore.Feature.Maps.Repositories
{
  using Foundation.SitecoreExtensions.Extensions;
  using System;
  using System.Collections.Generic;

  public class MapRepository : IMapRepository
  {

    public IEnumerable<Data.Items.Item> GetAll(Data.Items.Item contextItem)
    {
      if (contextItem == null)
      {
        throw new ArgumentNullException(nameof(contextItem));
      }
      if (contextItem.IsDerived(Templates.MapPoint.ID))
      {
        return new List<Data.Items.Item>() { contextItem };
      }
      if (!contextItem.IsDerived(Templates.MapPointsFolder.ID))
      {
        throw new ArgumentException("Item must derive from MapPointsFolder", nameof(contextItem));
      }

      return GetRecursive(contextItem);
    }

    //TODO: change to bucket search
    private IEnumerable<Data.Items.Item> GetRecursive(Data.Items.Item item)
    {
      var result = new List<Data.Items.Item>();

      foreach (Data.Items.Item childItem in item.Children)
      {
        if (childItem.IsDerived(Templates.MapPointsFolder.ID) && childItem.HasChildren)
        {
          result.AddRange(GetRecursive(childItem));
        }
        else if (childItem.IsDerived(Templates.MapPoint.ID))
        {
          result.Add(childItem);
        }
      }

      return result;
    }
  }
}
