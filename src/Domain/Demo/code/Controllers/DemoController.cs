using System.Web.Mvc;
using Habitat.Demo.Models;
using Sitecore.Analytics;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;

namespace Habitat.Demo.Controllers
{
  using System.Web.Mvc;
  using Habitat.Demo.Models;
  using Sitecore.Framework.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Controllers;

  public class DemoController : SitecoreController
  {
    public ActionResult VisitDetails()
    {
      if (Tracker.Current == null || Tracker.Current.Interaction == null)
        return null;
      return View("VisitDetails", new VisitInformation());
    }

    public ActionResult ContactDetails()
    {
      if (Tracker.Current == null || Tracker.Current.Contact == null)
        return null;
      return View("ContactDetails", new ContactInformation());
    }

    public ActionResult DemoContent()
    {
      if (RenderingContext.Current.ContextItem == null || !RenderingContext.Current.ContextItem.IsDerived(Templates.DemoContent.ID))
        return null;
      return View("DemoContent", new DemoContent(RenderingContext.Current.ContextItem));
    }

    public ActionResult EndVisit()
    {
      this.Session.Abandon();
      return this.Redirect("/");
    }
  }
}