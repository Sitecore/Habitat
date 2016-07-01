namespace Sitecore.Foundation.Forms.ActionEditors
{
  using System;
  using System.Collections.Specialized;
  using System.Web;
  using Sitecore.Form.Core.Utility;
  using Sitecore.Foundation.Forms.Services;
  using Sitecore.Web.UI.Pages;

  public abstract class BaseActionEditor : DialogForm
  {
    protected readonly ISheerService SheerService;

    protected BaseActionEditor(ISheerService sheerService)
    {
      this.SheerService = sheerService;
    }

    private NameValueCollection parametersCollection;

    private string ParametersXml => HttpContext.Current.Session[Web.WebUtil.GetQueryString("params")] as string;

    protected NameValueCollection Parameters => this.parametersCollection ?? (this.parametersCollection = ParametersUtil.XmlToNameValueCollection(this.ParametersXml));

    protected override void OnOK(object sender, EventArgs args)
    {
      var updatedParameters = ParametersUtil.NameValueCollectionToXml(this.Parameters);
      if (string.IsNullOrEmpty(updatedParameters))
      {
        updatedParameters = "-";
      }

      this.SheerService.SetDialogValue(updatedParameters);
      base.OnOK(sender, args);
    }
  }
}