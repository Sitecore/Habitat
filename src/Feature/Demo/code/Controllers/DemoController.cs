namespace Sitecore.Feature.Demo.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Analytics;
    using Sitecore.DependencyInjection;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.ExperienceExplorer.Core.State;
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
        public DemoStateService DemoStateService { get; }

        public DemoController(DemoStateService demoStateService)
        {
            this.DemoStateService = demoStateService;
        }

        public ActionResult ExperienceData()
        {
            if (Tracker.Current == null || Tracker.Current.Interaction == null || !this.DemoStateService.IsDemoEnabled)
            {
                return null;
            }
            var explorerContext = DependencyResolver.Current.GetService<IExplorerContext>();
            var isInExperienceExplorer = explorerContext?.IsExplorerMode() ?? false;
            if (Context.Site.DisplayMode != DisplayMode.Normal || WebEditUtility.IsDebugActive(Context.Site) || isInExperienceExplorer )
            {
                return new EmptyResult();
            }

            var experienceData = ServiceLocator.ServiceProvider.GetService<ExperienceData>();
            return this.View(experienceData);
        }

        public ActionResult ExperienceDataContent()
        {
            var experienceData = ServiceLocator.ServiceProvider.GetService<ExperienceData>();
            return this.View("_ExperienceDataContent", experienceData);
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