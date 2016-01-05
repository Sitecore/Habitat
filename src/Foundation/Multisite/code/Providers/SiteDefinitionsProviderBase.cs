namespace Sitecore.Foundation.MultiSite.Providers
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Web;

  public abstract class SiteDefinitionsProviderBase : ISiteDefinitionsProvider
  {
    public abstract IEnumerable<SiteDefinition> SiteDefinitions { get; }

    public virtual bool IsCurrent(string siteName)
    {
      return Sitecore.Context.Site.Name.Equals(siteName, StringComparison.OrdinalIgnoreCase);
    }
  }
}