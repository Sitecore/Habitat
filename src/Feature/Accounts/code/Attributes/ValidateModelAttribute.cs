namespace Sitecore.Feature.Accounts.Attributes
{
  using System.Web.Mvc;

  public class ValidateModelAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      var viewData = filterContext.Controller.ViewData;

      if (!viewData.ModelState.IsValid)
      {
        filterContext.Result = new ViewResult
                               {
                                 ViewData = viewData,
                                 TempData = filterContext.Controller.TempData
                               };
      }
    }
  }
}