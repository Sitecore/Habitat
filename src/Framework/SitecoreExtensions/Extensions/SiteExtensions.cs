namespace Habitat.Framework.SitecoreExtensions.Extensions
{
    using System;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Sites;

    public static class SiteExtensions
    {
        public static Item GetContextItem(this SiteContext site, ID derivedFromTemplateID)
        {
            if (site == null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            var startItem = site.Database.GetItem(Sitecore.Context.Site.StartPath);
            return startItem?.GetAncestorOrSelfOfTemplate(derivedFromTemplateID);
        }
    }
}