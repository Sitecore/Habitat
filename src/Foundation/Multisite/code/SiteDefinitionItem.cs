namespace Sitecore.Foundation.MultiSite
{
  using Sitecore.Data.Items;

  public class SiteDefinitionItem
  {
    public Item Item { get; set; }
    public string HostName { get; set; }

    public string Name { get; set; }

    public bool IsCurrent { get; set; }
  }
}