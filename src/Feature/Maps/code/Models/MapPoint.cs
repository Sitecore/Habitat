using Sitecore.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Feature.Maps.Models
{
  public class MapPoint
  {
    public MapPoint()
    {
      
    }
    public MapPoint(Data.Items.Item item)
    {
      Name = item[Templates.MapPoint.Fields.Name];
      Address = item[Templates.MapPoint.Fields.Address];
      Location = item[Templates.MapPoint.Fields.Location];
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }    
  }
}