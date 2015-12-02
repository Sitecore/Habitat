namespace Sitecore.Feature.Metadata.Repositories
{
  using Sitecore;
  using Sitecore.Data.Items;
  using Sitecore.Framework.SitecoreExtensions.Extensions;

  public static class MetadataRepository
  {
    public static Item Get(Item contextItem)
    {
      return contextItem.GetAncestorOrSelfOfTemplate(Templates.SiteMetadata.ID)
             ?? Context.Site.GetContextItem(Templates.SiteMetadata.ID);
    }
  }
}