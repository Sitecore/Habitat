namespace Sitecore.Feature.Identity.Repositories
{
  using Sitecore;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public static class IdentityRepository
  {
    public static Item Get(Item contextItem)
    {
      return contextItem.GetAncestorOrSelfOfTemplate(Templates.Identity.ID) ?? Context.Site.GetContextItem(Templates.Identity.ID);
    }
  }
}