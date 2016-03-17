using System.Web.Mvc;
using Sitecore.Foundation.Alerts.Models;

namespace Sitecore.Foundation.Alerts.Extensions
{
  public static class AlertControllerExtensions
  {
    public static ViewResult InfoMessage(this Controller controller, InfoMessage infoMessage)
    {
      if (infoMessage != null)
      {
        controller.ViewData.Model = infoMessage;
      }

      return new ViewResult()
      {
        ViewName = ViewPath.InfoMessage,
        ViewData = controller.ViewData,
        TempData = controller.TempData,
        ViewEngineCollection = controller.ViewEngineCollection
      };
    }
  }
}