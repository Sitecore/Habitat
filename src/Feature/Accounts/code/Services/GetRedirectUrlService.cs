namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Web;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class GetRedirectUrlService : IGetRedirectUrlService
    {
        private readonly IAccountsSettingsService accountsSettingsService;
        private const string ReturnUrlQuerystring = "ReturnUrl";

        public GetRedirectUrlService() : this(new AccountsSettingsService())
        {
        }

        public GetRedirectUrlService(IAccountsSettingsService accountsSettingsService)
        {
            this.accountsSettingsService = accountsSettingsService;
        }

        public string GetRedirectUrl(AuthenticationStatus status, string returnUrl = null)
        {
            var redirectUrl = this.GetDefaultRedirectUrl(status);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                redirectUrl = this.AddReturnUrl(redirectUrl, returnUrl);
            }

            return redirectUrl;
        }

        private string AddReturnUrl(string baseUrl, string returnUrl)
        {
           return baseUrl + "?" + ReturnUrlQuerystring + "=" + HttpUtility.UrlEncode(returnUrl);
        }

        public string GetDefaultRedirectUrl(AuthenticationStatus status)
        {
            switch (status)
            {
                case AuthenticationStatus.Unauthenticated:
                    return this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.LoginPage, Context.Site.GetRootItem());
                case AuthenticationStatus.Authenticated:
                    return this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage, Context.Site.GetRootItem());
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}