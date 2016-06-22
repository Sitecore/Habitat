namespace Sitecore.Feature.Teasers.Controller
{
  using System.Web.Mvc;
  using Sitecore.Feature.Teasers.Models;
  using Sitecore.Foundation.Alerts;
  using Sitecore.Foundation.Alerts.Extensions;
  using Sitecore.Foundation.Alerts.Models;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Presentation;

  public class TeasersController : Controller
  {
    public ActionResult GetDynamicContent(string viewName)
    {
      var dataSourceItem = RenderingContext.Current.Rendering.Item;
      if (!dataSourceItem?.IsDerived(Templates.DynamicTeaser.ID) ?? true)
      {
        return Context.PageMode.IsExperienceEditor ? this.InfoMessage(new InfoMessage(AlertTexts.InvalidDataSourceTemplateFriendlyMessage, InfoMessage.MessageType.Error)) : null;
      }

      var model = new DynamicTeaserModel(dataSourceItem);
      return this.View(viewName, model);
    }

    public ActionResult Accordion() => this.GetDynamicContent("Accordion");

    public ActionResult Tabs() => this.GetDynamicContent("Accordion");

    public ActionResult TeaserCarousel() => this.GetDynamicContent("TeaserCarousel");

    public ActionResult JumbotronCarousel() => this.GetDynamicContent("JumbotronCarousel");
  }
}