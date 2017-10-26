namespace Sitecore.Feature.Multisite.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Feature.Multisite.Repositories;

  public class MultisiteController : Controller
  {
    private readonly ISiteConfigurationRepository multisiteRepository;

    public MultisiteController(ISiteConfigurationRepository multisiteRepository)
    {
      this.multisiteRepository = multisiteRepository;
    }

    public ActionResult SwitchSite()
    {
      var definitions = this.multisiteRepository.Get();
      return this.View(definitions);
    }
  }
}