using System.Web.Mvc;
using Habitat.Navigation.Repositories;
using Sitecore.Mvc.Presentation;
using static System.String;

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

        public ActionResult LinkMenu()
        {
            if (IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
                return null;
            var item = RenderingContext.Current.Rendering.Item;
            var items = _navigationRepository.GetLinkMenuItems(item);
            return View("LinkMenu", items);
        }

    }
}