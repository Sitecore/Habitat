namespace Sitecore.Feature.Maps.Models
{
  using Data.Items;

  public class MapPoint
  {
    public MapPoint()
    {
    }

    public MapPoint(Item item)
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