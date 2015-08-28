using Sitecore.Mvc.Presentation;

namespace Habitat.Navigation.Controllers
{
    using System.Web.Mvc;

    public class NavigationController : Controller
    {
        private readonly INavigationService _navigationService;

        public NavigationController() : this(new NavigationService(RenderingContext.Current.Rendering.Item))
        {
        }

        public NavigationController(INavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        public ActionResult Breadcrumb()
        {
            var items = this._navigationService.GetBreadcrumb();
            return View("Breadcrumb", items);
        }

        public ActionResult PrimaryMenu()
        {
            var items = this._navigationService.GetPrimaryMenu();
            return View("PrimaryMenu", items);
        }

        public ActionResult SecondaryMenu()
        {
            var item = this._navigationService.GetSecondaryMenuItem();
            return View("SecondaryMenu", item);
        }
    }
}