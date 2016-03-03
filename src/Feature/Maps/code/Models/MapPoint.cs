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
      Name = item.Field(Templates.MapPoint.Fields.Name).ToString();
      Address = item.Field(Templates.MapPoint.Fields.Address).ToString();
      Location = item.Field(Templates.MapPoint.Fields.Location).ToString();
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }    
  }
}