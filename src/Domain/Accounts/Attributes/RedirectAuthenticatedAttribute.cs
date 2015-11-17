namespace Habitat.Accounts.Attributes
{
  using System;
  using System.Web.Mvc;
  using Habitat.Accounts.Services;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;

  public class RedirectAuthenticatedAttribute: Attribute,IAuthorizationFilter
  {
    private readonly IAccountsSettingsService accountsSettingsService;

    public RedirectAuthenticatedAttribute(): this(new AccountsSettingsService())
    {
    }

    public RedirectAuthenticatedAttribute(IAccountsSettingsService accountsSettingsService)
    {
      this.accountsSettingsService = accountsSettingsService;
    }

    public void OnAuthorization(AuthorizationContext filterContext)
    {
      if (Context.PageMode.IsNormal)
      {
        if (Context.User.IsAuthenticated)
        {
          var link = this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage, Context.Site.GetRootItem());
          filterContext.Result = new RedirectResult(link);
        }
      }
    }
  }
}