namespace Sitecore.Foundation.reCAPTCHA.Extensions
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class reCAPTCHAHtmlHelpers
    {
        public static MvcHtmlString reCAPTCHAwidget(this HtmlHelper helper)
        {
            return Context.PageMode.IsNormal ? helper.Partial(Constants.reCAPTCHAwidgetView) : new MvcHtmlString(string.Empty);
        }

    }
}