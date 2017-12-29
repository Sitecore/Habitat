namespace Sitecore.Feature.Sitemap.SitemapManager
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Sites;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Xml;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using System.Globalization;
    using Collections;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.ContentSearch.Utilities;
    using Data.Managers;
    using Globalization;
    using Links;
    using log4net;
    using Sitecore.Feature.Sitemap.Configuration;

    public class SitemapManager
    {
        private ILog log = LogManager.GetLogger("Sitemap XML");
        private Database _db = null;
        private static System.Collections.Specialized.StringDictionary m_Sites;
        public Database Db
        {
            get
            {
                if (_db == null)
                {
                    _db = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
                }

                return _db;
            }
        }

        private DeviceItem _defaultDevice = null;
        private DeviceItem DefaultDevice
        {
            get
            {
                if (_defaultDevice == null)
                {
                    _defaultDevice = Db.Resources.Devices.GetAll().Where(i => i.IsDefault).FirstOrDefault();
                }

                return _defaultDevice;
            }
        }

        public SitemapManager()
        {
            m_Sites = SitemapManagerConfiguration.GetSites();
            if (m_Sites.Count > 0)
            {
                foreach (DictionaryEntry site in m_Sites)
                {
                    if (!string.IsNullOrEmpty(site.Key.ToString()))
                    {
                        BuildSiteMap(site.Key.ToString(), site.Value.ToString());
                    }

                }
                BuildSiteMapIndex();
            }
        }

        private void BuildSiteMap(string sitename, string sitemapUrlNew)
        {
            if (!string.IsNullOrEmpty(sitename))
            {
                Site site = Sitecore.Sites.SiteManager.GetSite(sitename);
                if (site != null && !string.IsNullOrEmpty(sitemapUrlNew))
                {
                    SiteContext siteContext = Factory.GetSite(sitename);
                    string rootPath = siteContext.StartPath;

                    List<Item> items = GetSitemapItems(rootPath);

                    if (items.Count > 0)
                    {
                        string fullPath = MainUtil.MapPath(string.Concat("/", sitemapUrlNew));
                        string xmlContent = this.BuildSitemapXML(items, site, siteContext);
                        StreamWriter strWriter = new StreamWriter(fullPath, false);
                        strWriter.Write(xmlContent);
                        strWriter.Close();
                    }
                }
            }
        }

        private void BuildSiteMapIndex()
        {
            string fullPath = MainUtil.MapPath(string.Concat("/", SitemapManagerConfiguration.SitemapIndexFilename));
            string xmlContent = this.BuildSitemapIndexXML();

            StreamWriter strWriter = new StreamWriter(fullPath, false);
            strWriter.Write(xmlContent);
            strWriter.Close();
        }

        private string BuildSitemapIndexXML()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode declarationNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(declarationNode);
            XmlNode urlsetNode = doc.CreateElement("sitemapindex");
            XmlAttribute xmlnsAttr = doc.CreateAttribute("xmlns");
            xmlnsAttr.Value = SitemapManagerConfiguration.XmlnsTpl;
            urlsetNode.Attributes.Append(xmlnsAttr);

            doc.AppendChild(urlsetNode);

            foreach (DictionaryEntry siteEntry in m_Sites)
            {
                Site site = Sitecore.Sites.SiteManager.GetSite(siteEntry.Key.ToString());
                if (site != null)
                {
                    string filename = siteEntry.Value.ToString();

                    string serverUrl = SitemapManagerConfiguration.GetServerUrlBySite(site.Name);

                    doc = this.BuildSitemapIndexItem(doc, string.Format("{0}/{1}", serverUrl, filename));
                }
            }

            return doc.OuterXml;
        }

        private XmlDocument BuildSitemapIndexItem(XmlDocument doc, string filename)
        {
            string lastMod = HtmlEncode(System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

            XmlNode sitemapSetNode = doc.LastChild;

            XmlNode sitemapNode = doc.CreateElement("sitemap");
            sitemapSetNode.AppendChild(sitemapNode);

            XmlNode locNode = doc.CreateElement("loc");
            sitemapNode.AppendChild(locNode);
            locNode.AppendChild(doc.CreateTextNode(filename));

            XmlNode lastmodNode = doc.CreateElement("lastmod");
            sitemapNode.AppendChild(lastmodNode);
            lastmodNode.AppendChild(doc.CreateTextNode(lastMod));

            return doc;
        }

        public bool SubmitSitemapToSearchenginesByHttp()
        {
            if (!SitemapManagerConfiguration.IsProductionEnvironment)
                return false;

            bool result = false;
            Item sitemapConfig = Db.Items[SitemapManagerConfiguration.SitemapConfigurationItemPath];
            if (sitemapConfig != null)
            {
                string engines = sitemapConfig[Templates.SitemapSelector.Fields.SearchEngine];
                foreach (string id in engines.Split('|'))
                {
                    Item engine = Db.Items[id];
                    if (engine != null)
                    {
                        string engineHttpRequestString = engine[Templates.SitemapSearchEngine.Fields.HttpRequestString];
                        if (!string.IsNullOrEmpty(engineHttpRequestString))
                        {
                            foreach (string sitemapUrl in m_Sites.Values)
                                this.SubmitEngine(engineHttpRequestString, sitemapUrl);
                        }
                    }
                }
                result = true;
            }

            return result;
        }

        private string BuildSitemapXML(List<Item> items, Site site, SiteContext siteContext)
        {
            if (items.Count > 1 && site != null && siteContext != null)
            {
                XmlDocument doc = new XmlDocument();
                string url = "";

                XmlNode declarationNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(declarationNode);
                XmlNode urlsetNode = doc.CreateElement("urlset");
                XmlAttribute xmlnsAttr = doc.CreateAttribute("xmlns");
                xmlnsAttr.Value = SitemapManagerConfiguration.XmlnsTpl;
                urlsetNode.Attributes.Append(xmlnsAttr);

                doc.AppendChild(urlsetNode);
                Sitecore.Globalization.Language language = Language.Parse(siteContext.Language);
                Sitecore.Data.Database db = Sitecore.Configuration.Factory.GetDatabase("web");

                foreach (Item itm in items)
                {
                    if (itm != null)
                    {
                        // If an item version exists in the site's default language
                        if (Sitecore.Data.Managers.ItemManager.GetVersions(itm, language).Count == 0)
                        {
                            if (itm["__Enable item fallback"] == "1")
                            {
                                Language lang = fallback(itm, language, db);
                                if (lang != null)
                                {
                                    if (Sitecore.Data.Managers.ItemManager.GetVersions(itm, lang).Count == 0)
                                    {
                                        Language chainlang = fallback(itm, lang, db);
                                        if (chainlang != null)
                                        {
                                            language = lang;
                                        }
                                    }
                                    else
                                    {
                                        language = lang;
                                    }
                                }
                            }
                            else
                            {
                                language = Language.Parse(siteContext.Language);
                            }
                        }
                        if (language != null && Sitecore.Data.Managers.ItemManager.GetVersions(itm, language).Count > 0 && site != null)
                        {
                            url = HtmlEncode(this.GetItemUrl(itm, site));
                            if (!string.IsNullOrEmpty(url))
                            {
                                doc = this.BuildSitemapItem(doc, itm, site, url);
                            }
                        }
                    }
                }
                return doc.OuterXml;
            }
            return string.Empty;

        }
        private Language fallback(Item itm, Language language, Database db)
        {

            ID contextLanguageId = LanguageManager.GetLanguageItemId(language, db);
            Item contextLanguage = db.GetItem(contextLanguageId);
            string fallback = contextLanguage["Fallback Language"];
            if (!string.IsNullOrEmpty(fallback))
            {
                language = Language.Parse(fallback);
                return language;
            }
            return null;
        }
        private XmlDocument BuildSitemapItem(XmlDocument doc, Item item, Site site, string url)
        {
            string lastMod = HtmlEncode(item.Statistics.Updated.ToString("yyyy-MM-ddTHH:mm:sszzz"));

            XmlNode urlsetNode = doc.LastChild;

            XmlNode urlNode = doc.CreateElement("url");
            urlsetNode.AppendChild(urlNode);

            XmlNode locNode = doc.CreateElement("loc");
            urlNode.AppendChild(locNode);
            locNode.AppendChild(doc.CreateTextNode(url));

            XmlNode lastmodNode = doc.CreateElement("lastmod");
            urlNode.AppendChild(lastmodNode);
            lastmodNode.AppendChild(doc.CreateTextNode(lastMod));

            return doc;
        }

        private string GetItemUrl(Item item, Site site)
        {
            try
            {
                if (item != null && site != null)
                {
                    Sitecore.Links.UrlOptions options = Sitecore.Links.UrlOptions.DefaultOptions;
                    options.SiteResolving = Sitecore.Configuration.Settings.Rendering.SiteResolving;
                    options.Site = SiteContext.GetSite(site.Name);
                    options.AlwaysIncludeServerUrl = false;
                    string hostName = options.Site.HostName;
                    options.SiteResolving = true;
                    string targetHostName = options.Site.TargetHostName;
                    if (string.IsNullOrEmpty(targetHostName) && !string.IsNullOrEmpty(hostName))
                    {
                        targetHostName = options.Site.HostName;
                    }
                    else
                    {

                        hostName = targetHostName;
                    }
                    string serverUrl = SitemapManagerConfiguration.GetServerUrlBySite(site.Name);
                    string itemUrl = string.Empty;
                    try
                    {
                        itemUrl = Sitecore.Links.LinkManager.GetItemUrl(item, options);
                    }
                    catch (Exception ex)
                    {
                        log.Error("GetItemUrl Sitemap is null" + item.ID + ex.InnerException.ToString());
                    }
                    if (itemUrl != null && !string.IsNullOrEmpty(itemUrl))
                    {
                        itemUrl = itemUrl.Replace("://" + hostName, "");
                        if (itemUrl.StartsWith("http"))
                        {
                            return itemUrl;
                        }
                        else
                        {
                            return string.Format("{0}{1}", serverUrl, itemUrl);
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                log.Error("GetItemUrl Sitemap is null" + item.ID + ex.InnerException.ToString());
                return string.Empty;
            }


        }

        private static string HtmlEncode(string text)
        {
            string result = HttpUtility.HtmlEncode(text);

            return result;
        }

        private void SubmitEngine(string engine, string sitemapUrl)
        {
            //Check if it is not localhost because search engines returns an error
            if (!sitemapUrl.Contains("http://localhost"))
            {
                string request = string.Concat(engine, HtmlEncode(sitemapUrl));

                System.Net.HttpWebRequest httpRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(request);
                try
                {
                    System.Net.WebResponse webResponse = httpRequest.GetResponse();

                    System.Net.HttpWebResponse httpResponse = (System.Net.HttpWebResponse)webResponse;
                    if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        Log.Error(string.Format("Cannot submit sitemap to \"{0}\"", engine), this);
                    }
                }
                catch
                {
                    Log.Warn(string.Format("The serachengine \"{0}\" returns an 404 error", request), this);
                }
            }
        }


        private List<Item> GetSitemapItems(string rootPath)
        {
            if (!string.IsNullOrEmpty(rootPath))
            {
                Item contentRoot = Db.Items[rootPath];
                if (contentRoot == null)
                {
                    return new List<Item>();
                }

                List<Item> descendants;
                using (var context = ContentSearchManager.GetIndex((SitecoreIndexableItem)contentRoot).CreateSearchContext())
                {
                    descendants = context.GetQueryable<SearchResultItem>().Where(i => i.Path.Contains(contentRoot.Paths.FullPath)).Select(i => (Item)i.GetItem()).ToList();
                }
                if (descendants.Count > 0)
                {
                    descendants = descendants.Where(i => i[Templates.Sitemap.Fields.IncludeInSitemap.ToString()] == "1").Where(item => HasLayout(item, DefaultDevice)).GroupBy(p => p.ID).Select(g => g.First()).ToList();
                }

                descendants.Insert(0, contentRoot);
                return descendants;
            }
            return null;
        }

        private bool IncludeInSitemap(Item item)
        {
            return item.IsDerived(Templates.Sitemap.ID) && (MainUtil.GetBool(item[Templates.Sitemap.Fields.IncludeInSitemap], false));
        }

        private static bool HasLayout(Item item, DeviceItem device)
        {
            bool hasLayout = false;

            if (item != null && item.Visualization != null)
            {
                ID layoutId = item.Visualization.GetLayoutID(device);
                hasLayout = Guid.Empty != layoutId.Guid;
            }

            return hasLayout;
        }

        private List<string> BuildListFromString(string str, char separator)
        {
            string[] enabledTemplates = str.Split(separator);
            var selected = from dtp in enabledTemplates
                           where !string.IsNullOrEmpty(dtp)
                           select dtp;

            List<string> result = selected.ToList();

            return result;
        }
        private IEnumerable<Language> GetLanguages()
        {
            Sitecore.Data.Database db = Sitecore.Data.Database.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
            return LanguageManager.GetLanguages(db);
        }

    }
}