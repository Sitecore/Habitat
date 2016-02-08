namespace Sitecore.Foundation.SitecoreExtensions.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Data;

  public abstract class BaseRenderingController: Controller
  {
    private readonly ID datasourceTemplateId;

    protected BaseRenderingController(ID datasourceTemplateId)
    {
      this.datasourceTemplateId = datasourceTemplateId;
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!ID.IsNullOrEmpty(this.datasourceTemplateId))
      {
        
      }
    }
  }
}