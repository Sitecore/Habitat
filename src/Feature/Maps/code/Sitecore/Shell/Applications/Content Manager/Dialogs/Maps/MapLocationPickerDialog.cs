namespace Sitecore.Feature.Maps.Sitecore.Shell.Applications.Content_Manager.Dialogs.Maps
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using Web;

  [ExcludeFromCodeCoverage]
  public class MapLocationPickerDialog : Web.UI.Pages.DialogForm
  {
    #region Control

    public Web.UI.HtmlControls.Edit TxtSelectedLocation;

    #endregion

    #region Control Event

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      var value = WebUtil.GetQueryString("value");
      if (!string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(this.TxtSelectedLocation.Value))
      {
        this.TxtSelectedLocation.Value = value;
      }
    }

    protected override void OnOK(object sender, EventArgs args)
    {
      Web.UI.Sheer.SheerResponse.SetDialogValue(this.TxtSelectedLocation.Value);
      base.OnOK(sender, args);
    }

    #endregion
  }
}