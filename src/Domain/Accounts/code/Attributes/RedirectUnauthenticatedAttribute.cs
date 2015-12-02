namespace Sitecore.Feature.Accounts.Attributes
{
  using System.Web.Mvc;
  using Sitecore;
  using Sitecore.Framework.SitecoreExtensions.Extensions;

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