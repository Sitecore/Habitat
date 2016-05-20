﻿namespace Sitecore.Foundation.Multisite
{
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.Multisite;

  public class SiteContext
  {
    private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

    public SiteContext() : this(new SiteDefinitionsProvider())
    {    
    }

    public SiteContext(ISiteDefinitionsProvider siteDefinitionsProvider)
    {
      this.siteDefinitionsProvider = siteDefinitionsProvider;
    }

    public virtual SiteDefinition GetSiteDefinition([NotNull]Item item)
    {
      Assert.ArgumentNotNull(item, nameof(item));

      return this.siteDefinitionsProvider.GetContextSiteDefinition(item);
    }
  }
}