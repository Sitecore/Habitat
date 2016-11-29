namespace Sitecore.Feature.Navigation.Models
{
  using Sitecore.Data.Items;

  public class NavigationItem
  {
    public Item Item { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public int Level { get; set; }
    public NavigationItems Children { get; set; }
    public string Target { get; set; }
    public bool ShowChildren { get; set; }
  }
}