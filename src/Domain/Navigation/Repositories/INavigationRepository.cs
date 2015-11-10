namespace Habitat.Navigation.Repositories
{
  using Habitat.Navigation.Models;
  using Sitecore.Data.Items;

  public interface INavigationRepository
  {
    Item GetNavigationRoot(Item contextItem);
    NavigationItems GetBreadcrumb();
    NavigationItems GetPrimaryMenu();
    NavigationItem GetSecondaryMenuItem();
    NavigationItems GetLinkMenuItems(Item menuItem);
  }
}