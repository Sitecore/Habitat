namespace Sitecore.Feature.Demo.Controllers
{
  using System.Net;
  using System.Web.Mvc;
  using Sitecore.Analytics;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.Alerts.Exceptions;
  using Sitecore.Foundation.SitecoreExtensions.Attributes;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Mvc.Controllers;
  using Sitecore.Mvc.Presentation;

  [SkipAnalyticsTracking]
  public class DemoController : SitecoreController
  {
    private readonly IContactProfileProvider contactProfileProvider;
    private readonly IProfileProvider profileProvider;

    public DemoController() : this(new ContactProfileProvider(), new ProfileProvider())
    {
    }

    public DemoController(IContactProfileProvider contactProfileProvider, IProfileProvider profileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
      this.profileProvider = profileProvider;
    }

    public ActionResult ExperienceData()
    {
      if (Tracker.Current == null || Tracker.Current.Interaction == null)
      {
        return null;
      }
      return this.View(new ExperienceData(this.contactProfileProvider, this.profileProvider));
    }

    public ActionResult ExperienceDataContent()
    {
      return this.View("_ExperienceDataContent", new ExperienceData(this.contactProfileProvider, this.profileProvider));
    }


    public ActionResult DemoContent()
    {
      if (RenderingContext.Current.ContextItem == null || !RenderingContext.Current.ContextItem.IsDerived(Templates.DemoContent.ID))
      {
        throw new InvalidDataSourceItemException($"Item should be not null and derived from {nameof(Templates.DemoContent)} {Templates.DemoContent.ID} template");
      }

      return this.View("DemoContent", new DemoContent(RenderingContext.Current.ContextItem));
    }

    public ActionResult EndVisit()
    {
      this.Session.Abandon();
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }
  }
}