namespace Sitecore.Feature.Accounts.Attributes
{
    using System;
    using System.Web.Mvc;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class RedirectUnauthenticatedAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly IGetRedirectUrlService getRedirectUrlService;

        public RedirectUnauthenticatedAttribute() : this(new GetRedirectUrlService())
        {
        }

        private RedirectUnauthenticatedAttribute(IGetRedirectUrlService getRedirectUrlService)
        {
            this.getRedirectUrlService = getRedirectUrlService;
        }

        public void OnAuthorization(AuthorizationContext context)
        {
            if (Context.User.IsAuthenticated)
                return;
            var link = this.getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Unauthenticated, context.HttpContext.Request.RawUrl);
            context.Result = new RedirectResult(link);
        }
    }
}