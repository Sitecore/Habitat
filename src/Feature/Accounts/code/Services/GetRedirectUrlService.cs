namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Web;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    [Service(typeof(IGetRedirectUrlService))]
    public class GetRedirectUrlService : IGetRedirectUrlService
    {
        private readonly IAccountsSettingsService accountsSettingsService;
        private const string ReturnUrlQuerystring = "ReturnUrl";

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
                    return this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.LoginPage, Context.Site.GetStartItem());
                case AuthenticationStatus.Authenticated:
                    return this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage, Context.Site.GetStartItem());
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}