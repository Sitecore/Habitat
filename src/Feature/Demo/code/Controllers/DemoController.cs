namespace Sitecore.Feature.Demo.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Analytics;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Mvc.Controllers;
  using Sitecore.Mvc.Presentation;

  public class DemoController : SitecoreController
  {
    private readonly IContactProfileProvider contactProfileProvider;
    private readonly IProfileProvider profileProvider;

    public DemoController():this(new ContactProfileProvider(), new ProfileProvider())
    {
    }
    public DemoController(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;
    }

    public ActionResult VisitDetails()
    {
      if (Tracker.Current == null || Tracker.Current.Interaction == null)
        return null;
      return View("VisitDetails", new VisitInformation(profileProvider));
    }

    public ActionResult ContactDetails()
    {
      if (Tracker.Current == null || Tracker.Current.Contact == null)
        return null;
      return View("ContactDetails", new ContactInformation(contactProfileProvider));
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