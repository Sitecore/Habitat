namespace Habitat.Framework.SitecoreExtensions.Repositories
{
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public class ItemRepository
  {
    public static Item Get(ID id)
    {
      return DatabaseRepository.GetActiveDatabase().GetItem(id);
    }
  }
}