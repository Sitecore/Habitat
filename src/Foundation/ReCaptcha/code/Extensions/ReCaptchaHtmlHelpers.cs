namespace Sitecore.Foundation.ReCaptcha.Extensions
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class ReCaptchaHtmlHelpers
    {
        public static MvcHtmlString ReCaptchaJavaScript(this HtmlHelper helper)
        {
            return Context.PageMode.IsNormal ? helper.Partial(Constants.ReCaptchaScript) : new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString ReCaptchAwidget(this HtmlHelper helper)
        {
            return Context.PageMode.IsNormal ? helper.Partial(Constants.ReCaptcha2WidgetView) : new MvcHtmlString(string.Empty);
        }

    }
}