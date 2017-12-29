using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Sitecore.Feature.Sitemap.Models.Sitemap
{
    public class SitemapItem
    {
        public Item Item { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public IList<SitemapItem> Children { get; set; }
        public SitemapItems ChildItems { get; set; }
    }
}