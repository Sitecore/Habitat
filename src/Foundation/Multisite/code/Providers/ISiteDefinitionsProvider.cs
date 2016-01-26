namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using Sitecore.Web;

  public interface ISiteDefinitionsProvider
  {
    IEnumerable<SiteDefinition> SiteDefinitions { get; }
  }
}
