namespace Sitecore.Feature.Accounts.Attributes
{
  using System.Web.Mvc;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class AccountsRedirectAuthenticatedAttribute : RedirectAuthenticatedAttribute
  {
    private readonly IAccountsSettingsService accountsSettingsService;

    public AccountsRedirectAuthenticatedAttribute(IAccountsSettingsService accountsSettingsService)
    {
      this.accountsSettingsService = accountsSettingsService;
    }

    public AccountsRedirectAuthenticatedAttribute() : this(new AccountsSettingsService())
    {
    }

    protected override string GetRedirectUrl(ActionExecutingContext filterContext)
    {
      return this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage, Context.Site.GetRootItem());
    }
  }
}