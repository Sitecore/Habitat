namespace Sitecore.Foundation.SitecoreExtensions.Attributes
{
    using System.Web.Mvc;
    using Sitecore.Analytics;

    public class SkipAnalyticsTrackingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() && Tracker.IsActive)
            {
                Tracker.Current?.CurrentPage?.Cancel();
            }
        }
    }
}