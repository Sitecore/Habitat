namespace Sitecore.Foundation.Multisite
{
    using System;
    using Sitecore.Data.Items;
    using Sitecore.Web;

    public class SiteDefinition
    {
        private readonly Func<SiteInfo, bool> isCurrentSiteFunc;

        public SiteDefinition(Func<SiteInfo, bool> isCurrentSiteFunc)
        {
            this.isCurrentSiteFunc = isCurrentSiteFunc;
        }

        public Item Item { get; set; }
        public string HostName { get; set; }
        public string Name { get; set; }
        public bool IsCurrent => this.isCurrentSiteFunc(this.Site);
        public SiteInfo Site { get; set; }
    }
}