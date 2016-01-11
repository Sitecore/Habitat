namespace Sitecore.Feature.MultiSite.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Feature.MultiSite.Repositories;

  public class MultisiteController : Controller
  {
    private readonly ISiteConfigurationRepository multisiteRepository;

    public MultisiteController() :this(new SiteConfigurationRepository())
    {  
    }

    public MultisiteController(ISiteConfigurationRepository multisiteRepository)
    {
      this.multisiteRepository = multisiteRepository;
    }

    public ActionResult SwitchSite()
    {
      var definitions = multisiteRepository.Get();
      return this.View(definitions);
    }
  }
}