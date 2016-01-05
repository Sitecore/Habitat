namespace Sitecore.Feature.Accounts.Attributes
{
  using System;
  using System.Web.Mvc;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class RedirectAuthenticatedAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (Context.PageMode.IsNormal)
      {
        if (Context.User.IsAuthenticated)
        {
          var link = GetRedirectUrl(filterContext);
          if (filterContext.HttpContext.Request.RawUrl.Equals(link, StringComparison.InvariantCultureIgnoreCase))
          {
            link = RedirectUrl;
          }

          filterContext.Result = new RedirectResult(link);
        }
      }
    }

    protected virtual string GetRedirectUrl(ActionExecutingContext filterContext)
    {
      return RedirectUrl;
    }

    private string RedirectUrl => Context.Site.GetRootItem().Url();
  }
}