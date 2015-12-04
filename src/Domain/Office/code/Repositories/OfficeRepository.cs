namespace Habitat.Office.Repositories
{
  using System.Collections.Generic;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore.Data.Items;

  public class OfficeRepository : IOfficeRepository
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
        if (childItem.IsDerived(Templates.OfficeFolder.ID) && childItem.HasChildren)
        {
          result.AddRange(GetRecursive(childItem));
        }
        else if (childItem.IsDerived(Templates.Office.ID))
        {
          result.Add(childItem);
        }
      }

      return result;
    }
  }
}
