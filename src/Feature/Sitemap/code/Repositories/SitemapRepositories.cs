using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Feature.Sitemap.Models.Sitemap;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Data.Fields;
using Sitecore.ContentSearch;


namespace Sitecore.Feature.Sitemap.Repositories
{
    public class SitemapRepositories:ISitemapRepositories
    { 
        public Item ContextItem { get; }
        public Item SiteRoot { get;}
        public SitemapRepositories(Item contextItem)
        {
            this.ContextItem = contextItem;
            this.SiteRoot = this.GetSiteRoot(this.ContextItem);
        }
        public Item GetSiteRoot(Item contextItem)
        {
            return Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
          
        }
        
        public SitemapItems GetSitemapItems(Item contextItem)
        {
            var siteItems = this.ChildSitemapItems(this.SiteRoot, 0, 1);
            return siteItems;
        }


        public SitemapItems GetSitemapItemsForAjax(Item contextItem)
        {
            var siteItems = this.ChildSitemapItems(contextItem, 0, 1);
            return siteItems;
        }

        private void AddRootToSitemap(SitemapItems siteItems)
        {
            if (!this.IncludeInSitemap(this.SiteRoot))
            {
                return;
            }
        }
        
        private SitemapItem CreateSitemapItem(Item item, int level, int maxLevel = -1)
        {
            return new SitemapItem
            {
                Item = item,
                Url = item.Url(),
                ChildItems = this.ChildSitemapItems(item, level + 1, maxLevel)

            };
        }
       
        private SitemapItems ChildSitemapItems(Item parentItem, int level, int maxLevel)
        {

            if (!parentItem.HasChildren)
            {
                return null;
            }

            var childItems = parentItem.Children.Where(i => i.Versions.Count != 0).Where(item => IncludeInSitemap(item)).Select(i => this.CreateSitemapItem(i, level, maxLevel));
            return new SitemapItems
            {
                Items = childItems.ToList()
            };
        }

        private bool IncludeInSitemap(Item item)
        {
            return item.IsDerived(Templates.Sitemap.ID) && (MainUtil.GetBool(item[Templates.Sitemap.Fields.IncludeInSitemap], false));
        }
    }
}