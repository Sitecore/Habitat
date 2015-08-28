using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Habitat.Framework.SitecoreExtensions.Extensions;

namespace Habitat.News.Controller
{
    internal class NewsService : INewsService
    {
        public Item ContextItem { get; set; }

        public NewsService(Item contextItem)
        {
            if (contextItem == null)
                throw new ArgumentNullException(nameof(contextItem));
            if (!contextItem.IsDerived(Templates.NewsFolder.ID))
                throw new ArgumentException("Item must derive from NewsFolder", nameof(contextItem));
            this.ContextItem = contextItem;
        }

        public IEnumerable<Item> GetNews()
        {
            //TODO: Change to use buckets and search
            return this.ContextItem.Children.OrderByDescending(i => i[Templates.NewsArticle.Fields.Date]);
        }

        public IEnumerable<Item> GetLatestNews(int count)
        {
            //TODO: Change to use buckets and search
            return this.GetNews().Take(count);
        }
    }
}