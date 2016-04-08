namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
  using System;
  using System.Web;
  using System.Web.Mvc;
  using DynamicPlaceholders.Mvc.Extensions;
  using Sitecore.Data;
  using Sitecore.Data.IDTables;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.SitecoreExtensions.Controls;
  using Sitecore.Mvc.Helpers;
  using Sitecore.Shell.Framework.Commands.ContentEditor;

  /// <summary>
  ///   HTML Helper extensions
  /// </summary>
  public static class HtmlHelperExtensions
  {
    public static HtmlString ImageField(this SitecoreHelper helper, ID fieldID, int mh = 0, int mw = 0, string cssClass = null, bool disableWebEditing = false)
    {
      return helper.Field(fieldID.ToString(), new
      {
        mh,
        mw,
        DisableWebEdit = disableWebEditing,
        @class = cssClass ?? ""
      });
    }

    public static HtmlString ImageField(this SitecoreHelper helper, ID fieldID, Item item, int mh = 0, int mw = 0, string cssClass = null, bool disableWebEditing = false)
    {
      return helper.Field(fieldID.ToString(), item, new
      {
        mh,
        mw,
        DisableWebEdit = disableWebEditing,
        @class = cssClass ?? ""
      });
    }

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
      return useStaticPlaceholderNames ? helper.Placeholder(placeholderName) : SitecoreHelperExtensions.DynamicPlaceholder(helper, placeholderName);
    }

    public static HtmlString Field(this SitecoreHelper helper, ID fieldID)
    {
      Assert.ArgumentNotNullOrEmpty(fieldID, nameof(fieldID));
      return helper.Field(fieldID.ToString());
    }

    public static HtmlString Field(this SitecoreHelper helper, ID fieldID, object parameters)
    {
      Assert.ArgumentNotNullOrEmpty(fieldID, nameof(fieldID));
	  Assert.IsNotNull(parameters, nameof(parameters));
      return helper.Field(fieldID.ToString(), parameters);
    }

    public static HtmlString Field(this SitecoreHelper helper, ID fieldID, Item item, object parameters)
    {
      Assert.ArgumentNotNullOrEmpty(fieldID, nameof(fieldID));
      Assert.IsNotNull(item, nameof(item));
	  Assert.IsNotNull(parameters, nameof(parameters));
      return helper.Field(fieldID.ToString(), item, parameters);
    }

    public static HtmlString Field(this SitecoreHelper helper, ID fieldID, Item item)
    {
      Assert.ArgumentNotNullOrEmpty(fieldID, nameof(fieldID));
      Assert.IsNotNull(item, nameof(item));
      return helper.Field(fieldID.ToString(), item);
    }

    public static MvcHtmlString PageEditorError(this SitecoreHelper helper, string errorMessage)
    {
      Log.Error($@"Presentation error: {errorMessage}", typeof(HtmlHelperExtensions));

      if (Context.PageMode.IsNormal)
      {
        return new MvcHtmlString(string.Empty);
      }

      var builder = new TagBuilder("p");
      builder.AddCssClass("alert");
      builder.AddCssClass("alert-danger");
      builder.InnerHtml = errorMessage;

      return MvcHtmlString.Create(builder.ToString());
    }
  }
}