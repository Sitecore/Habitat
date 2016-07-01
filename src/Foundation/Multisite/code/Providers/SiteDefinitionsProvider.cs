namespace Sitecore.Foundation.Multisite.Providers
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Links;
  using Sitecore.Sites;
  using Sitecore.Web;

  public class SiteDefinitionsProvider : ISiteDefinitionsProvider
  {
    private IEnumerable<SiteDefinition> siteDefinitions;
    private IEnumerable<SiteInfo> sites;

    public SiteDefinitionsProvider() : this(SiteContextFactory.Sites)
    {
      
    }

    public SiteDefinitionsProvider(IEnumerable<SiteInfo> sites)
    {
      this.sites = sites;
    }

    public IEnumerable<SiteDefinition> SiteDefinitions => this.siteDefinitions ?? (this.siteDefinitions = this.sites.Where(this.IsValidSite).Select(this.Create).OrderBy(s => s.Item.Appearance.Sortorder).ToArray());

    private bool IsValidSite([NotNull] SiteInfo site)
    {
      return this.GetSiteRootItem(site) != null;
    }

    private Item GetSiteRootItem(SiteInfo site)
    {
      if (site == null)
        throw new ArgumentNullException(nameof(site));
      if (string.IsNullOrEmpty(site.Database))
        return null;
      var database = Database.GetDatabase(site.Database);
      var item = database?.GetItem(site.RootPath);
      if (item == null || !IsSite(item))
        return null;
      return item;
    }

    private SiteDefinition Create([NotNull] SiteInfo site)
    {
      if (site == null)
        throw new ArgumentNullException(nameof(site));

      var siteItem = this.GetSiteRootItem(site);
      return new SiteDefinition
             {
               Item = siteItem,
               Name = site.Name,
               HostName = GetHostName(site),
               IsCurrent = this.IsCurrent(site),
               Site = site
             };
    }

    private static string GetHostName(SiteInfo site)
    {
      if (!string.IsNullOrEmpty(site.TargetHostName))
        return site.TargetHostName;
      if (Uri.CheckHostName(site.HostName) != UriHostNameType.Unknown)
        return site.HostName;
      throw new ConfigurationErrorsException($"Cannot determine hostname for site '{site}'");
    }

    private static bool IsSite([NotNull] Item item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(item));
      return item.IsDerived(Templates.Site.ID);
    }

    private Item GetSiteItemByHierarchy(Item item)
    {
      return item.Axes.GetAncestors().FirstOrDefault(IsSite);
    }

    public SiteDefinition GetContextSiteDefinition(Item item)
    {
      return this.GetSiteByHierarchy(item) ?? this.SiteDefinitions.FirstOrDefault(s => s.IsCurrent);
    }

    private SiteDefinition GetSiteByHierarchy(Item item)
    {
      var siteItem = this.GetSiteItemByHierarchy(item);
      return siteItem == null ? null : this.SiteDefinitions.FirstOrDefault(s => s.Item.ID == siteItem.ID);
    }

    private bool IsCurrent(SiteInfo site)
    {
      return site != null && Context.Site != null && Context.Site.Name.Equals(site.Name, StringComparison.OrdinalIgnoreCase);
    }
  }
}