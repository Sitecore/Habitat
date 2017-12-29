using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Xml;

namespace Sitecore.Feature.Sitemap.Configuration 
{
    public class SitemapManagerConfiguration
    {
        #region properties



        public static string XmlnsTpl
        {
            get
            {
                return GetValueByName("xmlnsTpl");
            }
        }



        public static string WorkingDatabase
        {
            get
            {
                return GetValueByName("database");
            }
        }
        
        public static string SitemapConfigurationItemPath
        {
            get
            {
                return GetValueByName("sitemapConfigurationItemPath");
            }
        }

      
        public static bool IsProductionEnvironment
        {
            get
            {
                string production = GetValueByNameFromDatabase(Templates.SitemapXMLSettings.Fields.IsProduction.ToString());
                return !string.IsNullOrEmpty(production) && (production.ToLower() == "true" || production == "1");
            }
        }

       
        public static string SitemapIndexFilename
        {
            get
            {
                return GetValueByName("sitemapIndexFilename");
            }
        }

        public static string IndexName
        {
            get
            {
                return GetValueByName("indexName");
            }
        }
        #endregion properties

        private static string GetValueByName(string name)
        {
            string result = string.Empty;

            foreach (XmlNode node in Factory.GetConfigNodes("sitemapVariables/sitemapVariable"))
            {

                if (XmlUtil.GetAttribute("name", node) == name)
                {
                    result = XmlUtil.GetAttribute("value", node);
                    break;
                }
            }

            return result;
        }

        private static string GetValueByNameFromDatabase(string name)
        {
            string result = string.Empty;

            Database db = Factory.GetDatabase(WorkingDatabase);
            if (db != null)
            {
                Item configItem = db.Items[SitemapConfigurationItemPath];
                if (configItem != null)
                {
                    result = configItem[name];
                }
            }

            return result;
        }

        public static List<Item> GetSiteItems()
        {
            List<Item> siteItemList = new List<Item>();
            string siteField = GetValueByNameFromDatabase(Templates.SitemapSelector.Fields.Sites.ToString());
            if(!string.IsNullOrEmpty(siteField))
            {
                var siteItems = siteField.Split('|');
                if(siteItems!=null)
                {
                    Database db = Factory.GetDatabase(WorkingDatabase);
                    foreach (string site in siteItems)
                    {
                        siteItemList.Add(db.GetItem(site));
                    }
                }
            }
            return siteItemList;
        }

        public static StringDictionary GetSites()
        {
            StringDictionary sites = new StringDictionary();
            var siteItems = GetSiteItems();
            if(siteItems.Any())
            {
                foreach(Item itm in siteItems)
                {
                    if(!string.IsNullOrEmpty(itm[Templates.Sites.Fields.SiteName]) && !string.IsNullOrEmpty(itm[Templates.Sites.Fields.FileName]))
                    {
                        sites.Add(itm[Templates.Sites.Fields.SiteName], itm[Templates.Sites.Fields.FileName]);
                    }
                }

            }
            return sites;
        }

        public static string GetServerUrlBySite(string name)
        {
            string result = string.Empty;
            var siteItems = GetSiteItems();
            if (siteItems.Any())
            {
                Item itm = siteItems.Where(i=>i[Templates.Sites.Fields.SiteName].ToLower()==name.ToLower()).FirstOrDefault();
                if(itm !=null && !string.IsNullOrEmpty(itm[Templates.Sites.Fields.ServerURL]))
                {
                    result = itm[Templates.Sites.Fields.ServerURL];
                }
                
            }
            return result;
        }
    }
}
