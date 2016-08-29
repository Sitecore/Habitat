namespace Sitecore.Feature.Maps.Models
{
  public class MapPoint
  {
    public MapPoint()
    {
    }

    public MapPoint(Data.Items.Item item)
    {
      this.Name = Web.UI.WebControls.FieldRenderer.Render(item, Templates.MapPoint.Fields.Name.ToString());
      this.Address = Web.UI.WebControls.FieldRenderer.Render(item, Templates.MapPoint.Fields.Address.ToString());
      this.Location = item[Templates.MapPoint.Fields.Location];
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
  }
}