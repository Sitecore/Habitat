namespace Sitecore.Foundation.MultiSite
{
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.MultiSite.Providers;

  public class SiteContext
  {
    public virtual SiteDefinition GetSiteDefinition(Item item)
    {
      Assert.ArgumentNotNull(item, nameof(item));

      var itemSiteDefinitionsProvider = new ItemSiteDefinitionsProvider();
      var siteDefinition = itemSiteDefinitionsProvider.GetContextSiteDefinition(item);
      if (siteDefinition != null)
      {
        return siteDefinition;
      }
      var configSiteDefinitionsProvider = new ConfigurationSiteDefinitionsProvider();
      return configSiteDefinitionsProvider.GetContextSiteDefinition(item);
    }
  }
}