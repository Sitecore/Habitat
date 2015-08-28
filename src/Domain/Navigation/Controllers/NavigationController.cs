using System.Web.Mvc;
using Sitecore.Mvc.Presentation;

namespace Habitat.Navigation.Controllers
{
    public class NavigationController : Controller
    {
        private readonly INavigationService _navigationService;

        public NavigationController() : this(new NavigationService(RenderingContext.Current.Rendering.Item))
        {
        }

        public NavigationController(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ActionResult Breadcrumb()
        {
            var items = _navigationService.GetBreadcrumb();
            return View("Breadcrumb", items);
        }

        public ActionResult PrimaryMenu()
        {
            var items = _navigationService.GetPrimaryMenu();
            return View("PrimaryMenu", items);
        }

        public ActionResult SecondaryMenu()
        {
            var item = _navigationService.GetSecondaryMenuItem();
            return View("SecondaryMenu", item);
        }
    }
}