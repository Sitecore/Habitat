namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Collections.Generic;
  using Sitecore.Web;

  public class SiteDefinitionsProviderBase : ISiteDefinitionsProvider
  {
    public virtual IEnumerable<SiteDefinitionItem> SiteDefinitions { get; }
  }
}