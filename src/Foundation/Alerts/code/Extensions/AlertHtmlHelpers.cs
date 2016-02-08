using System.Web.Mvc;
using System.Web.Mvc.Html;
using Sitecore.Diagnostics;
using Sitecore.Foundation.Alerts.Models;

namespace Sitecore.Foundation.Alerts.Extensions
{
  public static class AlertHtmlHelpers
  {
    public static MvcHtmlString PageEditorError(this HtmlHelper helper, string errorMessage)
    {
      Log.Error($@"Presentation error: {errorMessage}", typeof(AlertHtmlHelpers));

      if (Context.PageMode.IsNormal)
      {
        return new MvcHtmlString(string.Empty);
      }

      return helper.Partial(ViewPath.InfoMessage, InfoMessage.Error(errorMessage));
    }
  }
}