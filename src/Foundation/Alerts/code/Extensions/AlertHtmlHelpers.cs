namespace Sitecore.Foundation.Alerts.Extensions
{
  using System.Web.Mvc;
  using System.Web.Mvc.Html;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Alerts.Models;
  using Sitecore.Data;

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

    public static MvcHtmlString PageEditorInfo(this HtmlHelper helper, string infoMessage)
    {
      if (Context.PageMode.IsNormal)
      {
        return new MvcHtmlString(string.Empty);
      }

      return helper.Partial(ViewPath.InfoMessage, InfoMessage.Info(infoMessage));
    }

    public static MvcHtmlString PageEditorError(this HtmlHelper helper, string errorMessage, string friendlyMessage, ID contextItemId, ID renderingId)
    {
      Log.Error($@"Presentation error: {errorMessage}, Context item ID: {contextItemId}, Rendering ID: {renderingId}", typeof(AlertHtmlHelpers));

      if (Context.PageMode.IsNormal)
      {
        return new MvcHtmlString(string.Empty);
      }

      return helper.Partial(ViewPath.InfoMessage, InfoMessage.Error(friendlyMessage));
    }
  }
}