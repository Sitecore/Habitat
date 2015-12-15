namespace Sitecore.Feature.Navigation.Controllers
{
  using System;
  using System.Web.Mvc;
  using Sitecore.Feature.Navigation.Repositories;
  using Sitecore.Mvc.Presentation;

  public class NavigationController : Controller
  {
    private readonly INavigationRepository _navigationRepository;

    public NavigationController() : this(new NavigationRepository(RenderingContext.Current.Rendering.Item))
    {
    }

    public NavigationController(INavigationRepository navigationRepository)
    {
      this._navigationRepository = navigationRepository;
    }

    public ActionResult Breadcrumb()
    {
      var items = this._navigationRepository.GetBreadcrumb();
      return this.View("Breadcrumb", items);
    }

    public ActionResult PrimaryMenu()
    {
      var items = this._navigationRepository.GetPrimaryMenu();
      return this.View("PrimaryMenu", items);
    }

    public ActionResult SecondaryMenu()
    {
      var item = this._navigationRepository.GetSecondaryMenuItem();
      return this.View("SecondaryMenu", item);
    }

    public ActionResult LinkMenu()
    {
      if (String.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
      {
        return null;
      }
      var item = RenderingContext.Current.Rendering.Item;
      var items = this._navigationRepository.GetLinkMenuItems(item);
      return this.View("LinkMenu", items);
    }
  }
}