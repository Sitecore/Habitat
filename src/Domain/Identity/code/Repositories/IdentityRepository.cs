namespace Habitat.Identity.Repositories
{
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Data.Items;

  public static class IdentityRepository
  {
    public static Item Get(Item contextItem)
    {
      return contextItem.GetAncestorOrSelfOfTemplate(Templates.Identity.ID) ?? Context.Site.GetContextItem(Templates.Identity.ID);
    }
  }
}