namespace Sitecore.Feature.Multisite.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Mvc;
  using Sitecore.Data;
  using Sitecore.Feature.Multisite.Models;

  public class MultisiteController : Controller
  {
    [HttpGet]
    public ActionResult SwitchSite(string SiteName)
    {
      var definitions = new SiteDefinitions();
      definitions.Sites = new List<SiteConfiguration> {new SiteConfiguration {HostName = "habitat.local", Name = "Habitat", IsCurrent = true}, new SiteConfiguration { HostName = "habitat.local1", Name = "Habitat1" } };
      return this.View(definitions);
    }
  }
}