namespace Sitecore.Foundation.Forms.Services
{
  using Sitecore.Web.UI.Sheer;

  public class SheerService : ISheerService
  {
    public void Alert(string alertMessage)
    {
      SheerResponse.Alert(alertMessage);
    }

    public void SetDialogValue(string dialogValue)
    {
      SheerResponse.SetDialogValue(dialogValue);
    }
  }
}