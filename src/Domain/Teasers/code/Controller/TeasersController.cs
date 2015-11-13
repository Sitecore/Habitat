namespace Habitat.Teasers.Controller
{
  using System.Web.Mvc;
  using Habitat.Teasers.Models;
  using Sitecore.Mvc.Presentation;

  public class TeasersController : Controller
  {
    public ActionResult AccordeonTeaser()
    {
      var model = new AccordeonModel(RenderingContext.Current.Rendering.Item);
      return this.View("AccordeonTeaser", model);
    }
  }
}