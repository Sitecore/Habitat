  namespace Habitat.Maps.Models
{
  using Sitecore.Data.Items;

  public class MapPoint
  {
    public MapPoint(Item item)
    {
      Name = item[Templates.MapPoint.Fields.Name];
      Address = item[Templates.MapPoint.Fields.Address];
      Latitude = item[Templates.MapPoint.Fields.Latitude];
      Longitude = item[Templates.MapPoint.Fields.Longitude];
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
  }
}