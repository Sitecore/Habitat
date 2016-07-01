namespace Sitecore.Feature.Teasers.Models
{
  using System;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;

  public class DynamicTeaserItem
  {
    public DynamicTeaserItem(Item headline) : this()
    {
      this.Item = headline;
      this.Title = headline[Templates.TeaserHeadline.Fields.Title];
      this.Icon = this.GetIcon(headline);
    }

    private string GetIcon(Item headline)
    {
      ReferenceField iconField = headline.Fields[Templates.TeaserHeadline.Fields.Icon];
      return iconField.TargetItem?[Templates.Icon.Fields.CssClass];
    }

    public DynamicTeaserItem()
    {
      this.HeaderId = $"header{Guid.NewGuid().ToString("N")}";
      this.PanelId = $"panel{Guid.NewGuid().ToString("N")}";
    }

    [CanBeNull]
    public Item Item { get; set; }

    [CanBeNull]
    public string Title { get; private set; }

    [CanBeNull]
    public string Icon { get; set; }

    public bool IsActive { get; set; }
    public string HeaderId { get; private set; }
    public string PanelId { get; private set; }
  }
}