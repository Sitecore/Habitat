namespace Sitecore.Feature.Multisite.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Mvc;
  using Sitecore.Data;
  using Sitecore.Feature.Multisite.Models;
  using Sitecore.Feature.Multisite.Repositories;

  public class MultisiteController : Controller
  {
    private IMultisiteRepository multisiteRepository;

    public MultisiteController() :this(new MultisiteRepository())
    {  
    }

    public MultisiteController(IMultisiteRepository multisiteRepository)
    {
      this.multisiteRepository = multisiteRepository;
    }

    [HttpGet]
    public ActionResult SwitchSite()
    {
      var definitions = multisiteRepository.GetSiteDefinitions();
      return this.View(definitions);
    }
  }
}