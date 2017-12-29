using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.ContentSearch;
using Sitecore.Feature.Sitemap.Configuration;
using log4net;

namespace Sitecore.Feature.Sitemap.SitemapManager
{
    public class SitemapHandler
    {
        private ILog log = LogManager.GetLogger("Sitemap XML");
        public void RefreshSitemap(object sender, EventArgs args)
        {
            try
            {
                Database db = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
                Item sitemapConfig = db.Items[SitemapManagerConfiguration.SitemapConfigurationItemPath];
                if (sitemapConfig != null && sitemapConfig[Templates.SitemapXMLSettings.Fields.GenerateSitempXML] == "1" && sitemapConfig[Templates.SitemapXMLSettings.Fields.GenerateSitempXMLOnPublishing] == "1")
                {
                    SitemapManager sitemapManager = new SitemapManager();
                    sitemapManager.SubmitSitemapToSearchenginesByHttp();
                }
            }
            catch (Exception ex)
            {
                log.Error("Sitemap is null" + ex.ToString());
            }
        }
        
        public void Execute(Item[] items, Sitecore.Tasks.CommandItem command, Sitecore.Tasks.ScheduleItem schedule)
        {
            try
            {
                Database db = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
                if (db != null)
                {
                    Item sitemapConfig = db.Items[SitemapManagerConfiguration.SitemapConfigurationItemPath];
                    if (sitemapConfig != null && sitemapConfig[Templates.SitemapXMLSettings.Fields.GenerateSitempXML] == "1" && sitemapConfig[Templates.SitemapXMLSettings.Fields.GenerateSitemapXMLByScheduler] == "1")
                    {  
                        SitemapManager sitemapManager = new SitemapManager();
                        sitemapManager.SubmitSitemapToSearchenginesByHttp();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Sitemap is null" + ex.ToString());
            }
        }
    }
}
