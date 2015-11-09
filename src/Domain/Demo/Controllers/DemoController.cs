using System.Web.Mvc;
using Habitat.Demo.Models;
using Sitecore.Mvc.Controllers;

namespace Habitat.Demo.Controllers
{
  public class DemoController : SitecoreController
  {
    public ActionResult VisitDetails()
    {
      /* Run the query and show the same view as IconAndTitleList */
      //VisitInformation visit = new VisitInformation();
      return View("VisitDetails", new VisitInformation());
    }

    public ActionResult ContactDetails()
    {
      /* Run the query and show the same view as IconAndTitleList */
      //VisitInformation visit = new VisitInformation();
      return View("ContactDetails", new ContactInformation());
    }

    public ActionResult EndVisit()
    {
      Session.Abandon();
      return Redirect("/");
    }
  }
}
