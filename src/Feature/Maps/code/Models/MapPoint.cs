namespace Sitecore.Feature.Maps.Models
{
  public class MapPoint
  {
    public MapPoint()
    {
    }

    public MapPoint(Data.Items.Item item)
    {
      this.Name = item[Templates.MapPoint.Fields.Name];
      this.Address = item[Templates.MapPoint.Fields.Address];
      this.Location = item[Templates.MapPoint.Fields.Location];
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
  }
}