namespace Habitat.Demo.Controllers
{
  using System.Web.Mvc;
  using Habitat.Demo.Models;
  using Sitecore.Mvc.Controllers;

  public class DemoController : SitecoreController
  {
    public ActionResult VisitDetails()
    {
      /* Run the query and show the same view as IconAndTitleList */
      //VisitInformation visit = new VisitInformation();
      return this.View("VisitDetails", new VisitInformation());
    }

    public ActionResult ContactDetails()
    {
      /* Run the query and show the same view as IconAndTitleList */
      //VisitInformation visit = new VisitInformation();
      return this.View("ContactDetails", new ContactInformation());
    }

    public ActionResult EndVisit()
    {
      this.Session.Abandon();
      return this.Redirect("/");
    }
  }
}