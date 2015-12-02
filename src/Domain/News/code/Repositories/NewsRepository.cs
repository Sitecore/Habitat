namespace Sitecore.Feature.News.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Framework.SitecoreExtensions.Extensions;

  internal class NewsRepository : INewsRepository
  {
    public Item ContextItem { get; set; }

    public NewsRepository(Item contextItem)
    {
      if (contextItem == null)
      {
        throw new ArgumentNullException(nameof(contextItem));
      }
      if (!contextItem.IsDerived(Templates.NewsFolder.ID))
      {
        throw new ArgumentException("Item must derive from NewsFolder", nameof(contextItem));
      }
      this.ContextItem = contextItem;
    }

    public IEnumerable<Item> Get()
    {
      //TODO: Change to use buckets and search
      return this.ContextItem.Children.OrderByDescending(i => i[Templates.NewsArticle.Fields.Date]);
    }

    public IEnumerable<Item> GetLatestNews(int count)
    {
      //TODO: Change to use buckets and search
      return this.Get().Take(count);
    }
  }
}