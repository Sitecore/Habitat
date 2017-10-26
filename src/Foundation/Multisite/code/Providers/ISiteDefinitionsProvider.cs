namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using Sitecore.Data.Items;
  using Sitecore.Web;

  public interface ISiteDefinitionsProvider
  {
    IEnumerable<SiteDefinition> SiteDefinitions { get; }
    SiteDefinition GetContextSiteDefinition(Item item);
  }
}
