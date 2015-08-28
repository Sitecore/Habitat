using System.Web.Mvc;
using Habitat.Navigation.Repositories;
using Sitecore.Mvc.Presentation;

namespace Habitat.Navigation.Controllers
{
    public class NavigationController : Controller
    {
        private readonly INavigationRepository _navigationRepository;

        public NavigationController() : this(new NavigationRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public NavigationController(INavigationRepository navigationRepository)
        {
            _navigationRepository = navigationRepository;
        }

        public ActionResult Breadcrumb()
        {
            var items = _navigationRepository.GetBreadcrumb();
            return View("Breadcrumb", items);
        }

        public ActionResult PrimaryMenu()
        {
            var items = _navigationRepository.GetPrimaryMenu();
            return View("PrimaryMenu", items);
        }

        public ActionResult SecondaryMenu()
        {
            var item = _navigationRepository.GetSecondaryMenuItem();
            return View("SecondaryMenu", item);
        }
    }
}