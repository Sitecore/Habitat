namespace Sitecore.Feature.Accounts.Attributes
{
  using System;
  using System.Web.Mvc;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class RedirectAuthenticatedAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!Context.PageMode.IsNormal)
        return;
      if (!Context.User.IsAuthenticated)
        return;
      var link = this.GetRedirectUrl(filterContext);
      if (filterContext.HttpContext.Request.RawUrl.Equals(link, StringComparison.InvariantCultureIgnoreCase))
      {
        link = this.RedirectUrl;
      }

      filterContext.Result = new RedirectResult(link);
    }

    protected virtual string GetRedirectUrl(ActionExecutingContext filterContext)
    {
      return this.RedirectUrl;
    }

    private string RedirectUrl => Context.Site.GetRootItem().Url();
  }
}