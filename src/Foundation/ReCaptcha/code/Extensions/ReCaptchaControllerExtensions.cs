namespace Sitecore.Foundation.ReCaptcha.Extensions
{
    using System.Web.Mvc;

    public static class ReCaptchaControllerExtensions
    {
        public static ViewResult ReCaptchAwidget(this Controller controller)
        {
            return new ViewResult
            {
                ViewName = Constants.ReCaptcha2WidgetView,
                ViewData = controller.ViewData,
                TempData = controller.TempData,
                ViewEngineCollection = controller.ViewEngineCollection
            };
        }

    }
}