namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
  using System.Web;
  using System.Web.Mvc;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Controls;
  using Sitecore.Mvc.Helpers;
  using Sitecore.Support;

  /// <summary>
  ///   HTML Helper extensions
  /// </summary>
  public static class HtmlHelperExtensions
  {
    public static HtmlString ImageField(this SitecoreHelper helper, string fieldName, Item item, int mh = 0, int mw = 0, string cssClass = null, bool disableWebEditing = false)
    {
      return helper.Field(fieldName, item, new
                                           {
                                             mh,
                                             mw,
                                             DisableWebEdit = disableWebEditing,
                                             @class = cssClass ?? ""
                                           });
    }

    public static EditFrameRendering BeginEditFrame<T>(this HtmlHelper<T> helper, string dataSource, string buttons)
    {
      var frame = new EditFrameRendering(helper.ViewContext.Writer, dataSource, buttons);
      return frame;
    }

    public static HtmlString DynamicPlaceholder(this SitecoreHelper helper, string placeholderName, bool useStaticPlaceholderNames = false)
    {
      if (useStaticPlaceholderNames)
        return helper.Placeholder(placeholderName);
      return DynamicPlaceholderExtension.DynamicPlaceholder(helper, placeholderName);
    }

    public static HtmlString Field(this SitecoreHelper helper, ID fieldID)
    {
      Assert.ArgumentNotNullOrEmpty(fieldID, nameof(fieldID));
      return helper.Field(fieldID.ToString());
    }
  }
}