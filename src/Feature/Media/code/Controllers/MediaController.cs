namespace Sitecore.Feature.Media.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Mvc;
  using Sitecore.Feature.Media.Infrastructure.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public class MediaController : Controller
  {
    public ActionResult SectionMedia()
    {
      var renderingPropertiesRepository = new RenderingPropertiesRepository();
      var mediamBackgroundModel = renderingPropertiesRepository.Get<MediaBackgroundRenderingModel>();

      return this.View(mediamBackgroundModel);
    }

    public ActionResult PageHeaderMedia()
    {
      var renderingPropertiesRepository = new RenderingPropertiesRepository();
      var mediamBackgroundModel = renderingPropertiesRepository.Get<MediaBackgroundRenderingModel>();

      return this.View(mediamBackgroundModel);
    }
  }
}