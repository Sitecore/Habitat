namespace Sitecore.Feature.Teasers.Models
{
    using System;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

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
      return headline?.TargetItem(Templates.TeaserHeadline.Fields.Icon)?[Templates.Icon.Fields.CssClass];
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