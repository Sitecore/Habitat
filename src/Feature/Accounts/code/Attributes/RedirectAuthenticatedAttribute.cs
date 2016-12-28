namespace Sitecore.Feature.Accounts.Attributes
{
    using System.Web.Mvc;
    using Sitecore.Feature.Accounts.Services;

    public class RedirectAuthenticatedAttribute : ActionFilterAttribute
    {
        private readonly IGetRedirectUrlService getRedirectUrlService;

        public RedirectAuthenticatedAttribute() : this(new GetRedirectUrlService())
        {
        }

        public RedirectAuthenticatedAttribute(IGetRedirectUrlService getRedirectUrlService)
        {
            this.getRedirectUrlService = getRedirectUrlService;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Context.PageMode.IsNormal)
                return;
            if (!Context.User.IsAuthenticated)
                return;
            var link = this.getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
            filterContext.Result = new RedirectResult(link);
        }
    }
}