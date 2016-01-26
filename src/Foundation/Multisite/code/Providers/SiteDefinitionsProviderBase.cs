namespace Sitecore.Foundation.Multisite.Providers
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Data.Items;

  public abstract class SiteDefinitionsProviderBase : ISiteDefinitionsProvider
  {
    public abstract IEnumerable<SiteDefinition> SiteDefinitions { get; }

    public virtual bool IsCurrent(string siteName)
    {
      return Context.Site != null && Context.Site.Name.Equals(siteName, StringComparison.OrdinalIgnoreCase);
    }

    public abstract SiteDefinition GetContextSiteDefinition(Item item);
  }
}