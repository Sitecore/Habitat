using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;

namespace Habitat.Metadata.Repositories
{
    public static class MetadataRepository
    {
        public static Item Get(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Templates.SiteMetadata.ID) 
                ?? Sitecore.Context.Site.GetContextItem(Templates.SiteMetadata.ID);
        }
    }
}
