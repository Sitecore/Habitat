using ContentEditor_Text = Sitecore.Shell.Applications.ContentEditor.Text;
using IContentField = Sitecore.Shell.Applications.ContentEditor.IContentField;
using Sitecore_Context = Sitecore.Context;

namespace Sitecore.Feature.Maps.Sitecore.Shell.Applications.ContentEditor.FieldTypes
{
  using System.Diagnostics.CodeAnalysis;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using Diagnostics;

  public class MapField : ContentEditor_Text, IContentField
  {
    private readonly int mapWidth = 600;
    private readonly int mapHeight = 150;
    private readonly int mapZoomFactor = 14;

    protected override void Render(HtmlTextWriter output)
    {
      Assert.ArgumentNotNull(output, nameof(output));

      base.Render(output);

      //render other control
      var value = this.GetValue();
      if (string.IsNullOrEmpty(value))
        return;
      //get lat lng
      var position = value.Split(',');

      if (position.Count() != 2)
        return;
      double lat = 0;
      double lng = 0;

      double.TryParse(position[0], out lat);
      double.TryParse(position[1], out lng);

      var mapImageCtrl = new Image
                         {
                           ID = this.ID + "_Img_MapView",
                           CssClass = "imageMapView",
                           Width = this.mapWidth,
                           Height = this.mapHeight,
                           ImageUrl = this.GetMapImageUrl()
                         };
      mapImageCtrl.Style.Add("padding-top", "5px");

      mapImageCtrl.RenderControl(output);
    }

    private string GetMapImageUrl()
    {
      return string.Format("http://maps.googleapis.com/maps/api/staticmap?center={0}&zoom={1}&size={2}x{3}&sensor=false&maptype=roadmap&&markers=color:blue%7Clabel:Location%7C{0}", this.GetValue(), this.mapZoomFactor, this.mapWidth, this.mapHeight);
    }

    public string GetValue()
    {
      return this.Value;
    }

    public void SetValue(string value)
    {
      this.Value = value;
    }

    public override void HandleMessage(Web.UI.Sheer.Message message)
    {
      if (message["id"] != this.ID || string.IsNullOrWhiteSpace(message.Name))
        return;

      switch (message.Name)
      {
        case "map:setLocation":
          Sitecore_Context.ClientPage.Start(this, "SetLocation");
          return;
        case "map:clearLocation":
          Sitecore_Context.ClientPage.Start(this, "ClearLocation");
          return;
      }

      if (this.Value.Length > 0)
      {
        this.SetModified();
      }

      this.Value = string.Empty;
    }

    protected void SetLocation(Web.UI.Sheer.ClientPipelineArgs args)
    {
      if (args.IsPostBack)
      {
        if (args.HasResult && this.Value.Equals(args.Result) == false)
        {
          this.SetModified();
          this.SetValue(args.Result);
        }
      }
      else
      {
        //show popup        
        var url = UIUtil.GetUri("control:MapLocationPickerDialog");
        var value = this.GetValue();
        if (!string.IsNullOrEmpty(value))
        {
          url = $"{url}&value={value}";
        }
        Web.UI.Sheer.SheerResponse.ShowModalDialog(url, "800", "600", "", true);
        args.WaitForPostBack();
      }
    }

    protected void ClearLocation(Web.UI.Sheer.ClientPipelineArgs args)
    {
      this.SetValue(string.Empty);
      this.SetModified();
    }
  }
}