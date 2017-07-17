using System;
using System.Collections.Generic;
using Sitecore.Mvc.Presentation;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Sitemap.Models.Sitemap
{
    using Sitecore.Data.Items;
    public class SitemapItems : RenderingModel
    {   
        public IList<SitemapItem> Items { get; set; }

        public SitemapItem CurrentItem { get; set; }
    }
}