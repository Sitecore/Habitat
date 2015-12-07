namespace Habitat.Maps.Repositories
{
  using System.Collections.Generic;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class MapRepository : IMapRepository
  {
    public IEnumerable<Item> GetAll(Item contextItem)
    {
      return GetRecursive(contextItem);
    }

    //TODO: change to bucket search
    private IEnumerable<Item> GetRecursive(Item item)
    {
      var result = new List<Item>();
      
      foreach (Item childItem in item.Children)
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
