  namespace Habitat.Office.Models
{
  using Sitecore.Data.Items;

  public class Office
  {
    public Office(Item item)
    {
      Name = item[Templates.Office.Fields.Name];
      Address = item[Templates.Office.Fields.Address];
      Latitude = item[Templates.Office.Fields.Latitude];
      Longitude = item[Templates.Office.Fields.Longitude];
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
  }
}