namespace Sitecore.Feature.MultiSite.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Feature.MultiSite.Repositories;

  public class MultisiteController : Controller
  {
    private readonly ISiteDefinitionRepositoryRepository multisiteRepository;

    public MultisiteController() :this(new SiteDefinitionRepositoryRepository())
    {  
    }

    public MultisiteController(ISiteDefinitionRepositoryRepository multisiteRepository)
    {
      this.multisiteRepository = multisiteRepository;
    }

    [HttpGet]
    public ActionResult SwitchSite()
    {
      var definitions = multisiteRepository.Get();
      return this.View(definitions);
    }
  }
}