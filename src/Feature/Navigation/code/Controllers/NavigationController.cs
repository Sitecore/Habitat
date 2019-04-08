﻿namespace Sitecore.Feature.Navigation.Controllers
{
    using System.Web.Mvc;
    using Sitecore.Feature.Navigation.Repositories;
    using Sitecore.Foundation.Alerts.Extensions;
    using Sitecore.Foundation.Alerts.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Mvc.Presentation;

    public class NavigationController : Controller
    {
        private readonly INavigationRepository navigationRepository;

        public NavigationController(INavigationRepository navigationRepository)
        {
            this.navigationRepository = navigationRepository;
        }

        public ActionResult Breadcrumb()
        {
            var items = this.navigationRepository.GetBreadcrumb();
            return this.View("Breadcrumb", items);
        }

        public ActionResult PrimaryMenu()
        {
            var items = this.navigationRepository.GetPrimaryMenu();
            return this.View("PrimaryMenu", items);
        }

        public ActionResult SecondaryMenu()
        {
            var item = this.navigationRepository.GetSecondaryMenuItem();
            return this.View("SecondaryMenu", item);
        }

        public ActionResult NavigationLinks()
        {
            if (string.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
            {
                return null;
            }
            var item = RenderingContext.Current.Rendering.Item;
            var items = this.navigationRepository.GetLinkMenuItems(item);
            return this.View(items);
        }

        public ActionResult LinkMenu()
        {
            if (string.IsNullOrEmpty(RenderingContext.Current.Rendering.DataSource))
            {
                return Context.PageMode.IsExperienceEditor ? this.InfoMessage(new InfoMessage(DictionaryPhraseRepository.Current.Get("/Navigation/Link Menu/No Items", "This menu has no items."), InfoMessage.MessageType.Warning)) : null;
            }
            var item = RenderingContext.Current.Rendering.Item;
            var items = this.navigationRepository.GetLinkMenuItems(item);
            return this.View("LinkMenu", items);
        }
    }
}