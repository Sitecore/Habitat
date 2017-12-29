using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Sitemap.Repositories
{
    using Data.Items;
    using Sitecore.Feature.Sitemap.Models.Sitemap;
    
    public interface ISitemapRepositories
    {
        Item GetSiteRoot(Item contextItem);
        SitemapItems GetSitemapItems(Item contextItem);

        SitemapItems GetSitemapItemsForAjax(Item contextItem);

    }
}
