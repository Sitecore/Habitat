﻿namespace Sitecore.Feature.Teasers.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class DynamicTeaserModel
  {
    private DynamicTeaserItem[] _items;

    public DynamicTeaserModel([NotNull] Item dynamicTeaser)
    {
      if (dynamicTeaser == null)
      {
        throw new ArgumentNullException(nameof(dynamicTeaser));
      }
      this.Item = dynamicTeaser;
      this.Id = $"accordion-{Guid.NewGuid().ToString("N")}";
    }

    public Item Item { get; set; }

    public string Id { get; private set; }

    public IEnumerable<DynamicTeaserItem> Items
    {
      get
      {
        if (this._items != null)
        {
          return this._items;
        }
        this._items = this.CreateDynamicTeaserItems();
        this.SetActiveItem(this._items);
        return this._items;
      }
    }

    private DynamicTeaserItem[] CreateDynamicTeaserItems()
    {
      var childItems = this.Item.Children.Where(i => i.DescendsFrom(Templates.TeaserHeadline.ID)).ToArray();
      DynamicTeaserItem[] returnItems = {};
      if (childItems.Any())
      {
        returnItems = childItems.Select(i => new DynamicTeaserItem(i)).ToArray();
      }
      else
      {
        var count = this.Item.GetInteger(Templates.DynamicTeaser.Fields.Count);
        if (count.HasValue)
        {
          returnItems = new object[count.Value].Select(o => new DynamicTeaserItem()).ToArray();
        }
      }
      return returnItems;
    }

    private void SetActiveItem(IReadOnlyList<DynamicTeaserItem> items)
    {
      if (!items.Any())
        return;
      var active = this.Item.GetInteger(Templates.DynamicTeaser.Fields.Active);
      if (active.HasValue)
      {
        var activeItem = items.ElementAtOrDefault(active.Value);
        if (activeItem != null)
        {
          activeItem.IsActive = true;
          return;
        }
      }
      items[0].IsActive = true;
    }
  }
}