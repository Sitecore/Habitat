namespace Habitat.Metadata.Repositories
{
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Data.Items;

  public static class MetadataRepository
  {
    public static Item Get(Item contextItem)
    {
      return contextItem.GetAncestorOrSelfOfTemplate(Templates.SiteMetadata.ID)
             ?? Context.Site.GetContextItem(Templates.SiteMetadata.ID);
    }
  }
}