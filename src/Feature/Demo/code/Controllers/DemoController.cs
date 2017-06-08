namespace Sitecore.Feature.Demo.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Sitecore.Analytics;
    using Sitecore.Data.Items;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.ExperienceExplorer.Business.Managers;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Feature.Demo.Services;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.Alerts.Exceptions;
    using Sitecore.Foundation.SitecoreExtensions.Attributes;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Sites;

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
            if (Tracker.Current == null || Tracker.Current.Interaction == null || this.IsDemoDisabled)
            {
                return null;
            }
            if (Context.Site.DisplayMode != DisplayMode.Normal || ModuleManager.IsExpViewModeActive || WebEditUtility.IsDebugActive(Context.Site))
                return new EmptyResult();

            return this.View(new ExperienceData(this.contactProfileProvider, this.profileProvider));
        }

        public bool IsDemoDisabled => this.HttpContext?.Request?.Headers["X-DisableDemo"]?.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase) ?? false;

        public ActionResult ExperienceDataContent()
        {
            return this.View("_ExperienceDataContent", new ExperienceData(this.contactProfileProvider, this.profileProvider));
        }

        public ActionResult DemoContent()
        {
            var item = RenderingContext.Current?.Rendering?.Item ?? RenderingContext.Current?.ContextItem;
            if (item == null || !item.IsDerived(Templates.DemoContent.ID))
            {
                throw new InvalidDataSourceItemException($"Item should be not null and derived from {nameof(Templates.DemoContent)} {Templates.DemoContent.ID} template");
            }

            var demoContent = new DemoContent(item);
            return this.View("DemoContent", demoContent);
        }

        public ActionResult EndVisit()
        {
            this.Session.Abandon();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}