namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using Sitecore.Web;

  public interface ISiteDefinitionsProvider
  {
    IEnumerable<SiteDefinitionItem> SiteDefinitions { get; }
  }
}
