namespace Sitecore.Foundation.reCAPTCHA.Extensions
{
    using System.Web.Mvc;

    public static class reCAPTCHAControllerExtensions
    {
        public static ViewResult reCAPTCHAwidget(this Controller controller)
        {
            return new ViewResult
            {
                ViewName = Constants.reCAPTCHAwidgetView,
                ViewData = controller.ViewData,
                TempData = controller.TempData,
                ViewEngineCollection = controller.ViewEngineCollection
            };
        }
    }
}