namespace Habitat.Office.Repositories
{
  using System.Collections.Generic;
  using Sitecore.Data.Items;

  public interface IOfficeRepository
  {
    IEnumerable<Item> GetAll(Item contextItem);
  }
}