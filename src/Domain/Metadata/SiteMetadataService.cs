using Sitecore.Data.Items;
using Habitat.Framework.SitecoreExtensions.Extensions;

namespace Habitat.Metadata
{
    public class SiteMetadataService
    {
        public static Item GetSiteMetadata(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Templates.SiteMetadata.ID) ?? Sitecore.Context.Site.GetContextItem(Templates.SiteMetadata.ID);
        }
    }
}
