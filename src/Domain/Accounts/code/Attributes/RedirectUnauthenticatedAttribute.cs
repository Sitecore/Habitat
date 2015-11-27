namespace Habitat.Accounts.Attributes
{
  using System;
  using System.Web.Mvc;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;

  public class RedirectUnauthenticatedAttribute: ActionFilterAttribute, IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationContext filterContext)
    {
      if (!Context.User.IsAuthenticated)
      {
        filterContext.Result = new RedirectResult(Context.Site.GetRootItem().Url());
      }
    }
  }
}