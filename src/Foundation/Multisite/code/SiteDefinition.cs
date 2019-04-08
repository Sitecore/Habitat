namespace Sitecore.Foundation.Multisite
{
  using Sitecore.Data.Items;
  using Sitecore.Web;

  public class SiteDefinition
  {
    public Item Item { get; set; }
    public string HostName { get; set; }
    public string Name { get; set; }
    public bool IsCurrent { get; set; }
    public SiteInfo Site { get; set; }
  }
}