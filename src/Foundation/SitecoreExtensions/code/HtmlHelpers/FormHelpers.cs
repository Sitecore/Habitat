namespace Sitecore.Foundation.SitecoreExtensions.HtmlHelpers
{
  using System;
  using System.Linq.Expressions;
  using System.Web.Mvc;
  using System.Web.Mvc.Html;
  using Sitecore.Mvc;

  public static class FormHelpers
  {
    public static MvcHtmlString FormHandler(this HtmlHelper htmlHelper)
    {
      var uid = htmlHelper.Sitecore().CurrentRendering?.UniqueId;
      return !uid.HasValue ? null : htmlHelper.Hidden("uid", uid.Value);
    }

  }
}