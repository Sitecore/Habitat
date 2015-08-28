using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;

namespace Habitat.Identity.Repositories
{
    public static class IdentityRepository
    {
        public static Item Get(Item contextItem)
        {
            return contextItem.GetAncestorOrSelfOfTemplate(Templates.Identity.ID) 
                ?? Sitecore.Context.Site.GetContextItem(Templates.Identity.ID);
        }
    }
}
