namespace Sitecore.Feature.Teasers.Controller
{
  using System;
  using System.Web.Mvc;
  using Sitecore.Feature.Teasers.Models;
  using Sitecore.Foundation.Alerts;
  using Sitecore.Foundation.Alerts.Extensions;
  using Sitecore.Foundation.Alerts.Models;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Mvc.Presentation;

  public class TeasersController : Controller
  {
    public ActionResult Accordion()
    {
      var dataSourceItem = RenderingContext.Current.Rendering.Item;
      if (!dataSourceItem?.IsDerived(Templates.DynamicTeaser.ID) ?? true)
      {
        return Context.PageMode.IsExperienceEditor ? this.InfoMessage(new InfoMessage(AlertTexts.InvalidDataSourceTemplateFriendlyMessage, InfoMessage.MessageType.Error)) : null;
      }

      var model = new DynamicTeaserModel(dataSourceItem);
      return this.View("Accordion", model);
    }

    public ActionResult Tabs()
    {
      var dataSourceItem = RenderingContext.Current.Rendering.Item;
      if (!dataSourceItem?.IsDerived(Templates.DynamicTeaser.ID) ?? true)
      {
        return Context.PageMode.IsExperienceEditor ? this.InfoMessage(new InfoMessage(AlertTexts.InvalidDataSourceTemplateFriendlyMessage, InfoMessage.MessageType.Error)) : null;
      }

      var model = new DynamicTeaserModel(dataSourceItem);
      return this.View("Tabs", model);
    }

    public ActionResult Carousel()
    {
      var dataSourceItem = RenderingContext.Current.Rendering.Item;
      if (!dataSourceItem?.IsDerived(Templates.DynamicTeaser.ID) ?? true)
      {
        return Context.PageMode.IsExperienceEditor ? this.InfoMessage(new InfoMessage(AlertTexts.InvalidDataSourceTemplateFriendlyMessage, InfoMessage.MessageType.Error)) : null;
      }

      var model = new DynamicTeaserModel(dataSourceItem);
      return this.View("Carousel", model);
    }
  }
}