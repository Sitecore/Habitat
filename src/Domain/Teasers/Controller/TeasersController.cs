using System.Web.Mvc;
using Habitat.Teasers.Models;
using Sitecore.Mvc.Presentation;

namespace Habitat.Teasers.Controller
{
    public class TeasersController : System.Web.Mvc.Controller
    {
        public ActionResult AccordeonTeaser()
        {
            AccordeonModel model = new AccordeonModel(RenderingContext.Current.Rendering.Item);
            return View("AccordeonTeaser", model);
        }
    }
}
